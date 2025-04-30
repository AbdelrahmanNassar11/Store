using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController(IServiceManager serviceManager) : ControllerBase
    {
        [HttpGet] // BaseUrl/api/products  
        public async Task<IActionResult> GetAllProducts([FromQuery] ProductSpecficationParamters specParams)
        {
            var result = await serviceManager.ProductServices.GetAllProductsAsync(specParams);
            if (result == null) BadRequest(); //400  
            return Ok(result);
        }

        [HttpGet("{id}")] // BaseUrl/api/products/{id}  
        public async Task<IActionResult> GetProductById(int id)
        {
            var result = await serviceManager.ProductServices.GetProductByIdAsync(id);
            if (result == null) NotFound(); //404  
            return Ok(result);
        }

        [HttpGet("Brands")]
        public async Task<IActionResult> GetAllBrands()
        {
            var result = await serviceManager.ProductServices.GetAllBrandsAsync();
            if (result == null) BadRequest(); //400  
            return Ok(result);
        }

        [HttpGet("Types")]
        public async Task<IActionResult> GetAllTypes()
        {
            var result = await serviceManager.ProductServices.GetAllTypesAsync();
            if (result == null) BadRequest(); //400  
            return Ok(result);
        }
    }
}
