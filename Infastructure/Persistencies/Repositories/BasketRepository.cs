using Domain.Entities;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Persistencies.Repositories
{
    //IConnectionMultiplexer connection دا البيمثل ال connection مع ال redis
    public class BasketRepository(IConnectionMultiplexer connection) : IBasketRepository
    {
        private readonly IDatabase _database = connection.GetDatabase();
        public async Task<CustomerBasket?> GetBasketAsync(string Id)
        {
            var redisValue = await _database.StringGetAsync(Id);
            if (redisValue.IsNullOrEmpty) return null;

            var basket = JsonSerializer.Deserialize<CustomerBasket>(redisValue); 
            
            if(basket == null) return null;
            return basket;
        }

        public async Task<CustomerBasket?> UpdateBasketAsync(CustomerBasket basket, TimeSpan? timeToLive = null)
        {
            var redisValue = JsonSerializer.Serialize(basket);
            var flag = _database.StringSet(basket.Id,redisValue, TimeSpan.FromDays(30));
            return flag? await GetBasketAsync(basket.Id) : null;
        }
        public async Task<bool> DeleteBasketAsync(string Id)
        {
            return await _database.KeyDeleteAsync(Id);
            
        }
    }
}
