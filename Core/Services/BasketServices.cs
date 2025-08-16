using AutoMapper;
using Domain.Entities;
using Domain.Exceptions;
using Persistencies.Repositories;
using Services.Abstractions;
using Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class BasketServices(IBasketRepository basketRepository,IMapper mapper) : IBasketServices
    {
        public async Task<BasketItemDto> GetBasketAsync(string id)
        {
           var basket = await basketRepository.GetBasketAsync(id);
           if (basket is null) throw new BasketNotFoundExceptions(id);
           var result = mapper.Map<BasketItemDto>(basket);
           return result;

        }

        public async Task<BasketItemDto> UpdateBasketAsync(BasketItemDto basketItemDto)
        {
            var basket = mapper.Map<CustomerBasket>(basketItemDto);
            basket = await basketRepository.UpdateBasketAsync(basket);
            if (basket is null) throw new BasketBadRequestExceptions();
            var result = mapper.Map<BasketItemDto>(basket);
            return result;
        }
        public async Task<bool> DeleteBasketAsync(string id)
        {
            var falg = await basketRepository.DeleteBasketAsync(id);
            if (falg is false) throw new BasketDeleteBadRequestException();
            return falg;
        }
    }
}
