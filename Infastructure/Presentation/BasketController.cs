using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;
using Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation
{
    [ApiController]
    [Route("api/[controller]")]
    public class BasketController(IServiceManager serviceManager) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetBasketById(string id)
        {
            var result = await serviceManager.BasketServices.GetBasketAsync(id);
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateBasketAsync(BasketItemDto basket)
        {
            var result = await serviceManager.BasketServices.UpdateBasketAsync(basket);
            return Ok(result);
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteBasketAsync(string id)
        {
            await serviceManager.BasketServices.DeleteBasketAsync(id);
            return NoContent(); // 204
        }
    }
}
