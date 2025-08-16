using Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstractions
{
    public interface IBasketServices
    {
        Task<BasketItemDto> GetBasketAsync(string id);
        Task<BasketItemDto> UpdateBasketAsync(BasketItemDto basketItemDto);
        Task<bool> DeleteBasketAsync(string id);

    }
}
