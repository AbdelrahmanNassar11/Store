using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Presentation.Attributes;
using Services.Abstractions;
using Shared;
using Shared.Dtos;
using Shared.ErrorModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation
{
    [ApiController]
    [Route("api/[controller]")]
    [Cache(100)]
    public class ProductsController(IServiceManager serviceManager) : ControllerBase
    {
        [HttpGet] // BaseUrl/api/products  
        [ProducesResponseType(typeof(PaginationResponse<ProductResultDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<PaginationResponse<ProductResultDto>>> GetAllProducts([FromQuery] ProductSpecficationParamters specParams)
        {
            var result = await serviceManager.ProductServices.GetAllProductsAsync(specParams);
            
            return Ok(result);
        }

        [HttpGet("{id}")] // BaseUrl/api/products/{id}  
        [ProducesResponseType(typeof(ProductResultDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductResultDto>> GetProductById(int id)
        {
            var result = await serviceManager.ProductServices.GetProductByIdAsync(id);
            if (result == null) NotFound(); //404  
            return Ok(result);
        }

        [HttpGet("Brands")]
        [ProducesResponseType(typeof(IEnumerable<BrandResultDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<BrandResultDto>> GetAllBrands()
        {
            var result = await serviceManager.ProductServices.GetAllBrandsAsync();
            if (result == null) BadRequest(); //400  
            return Ok(result);
        }

        [HttpGet("Types")]
        [ProducesResponseType(typeof(IEnumerable<TypeResultDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<TypeResultDto>> GetAllTypes()
        {
            var result = await serviceManager.ProductServices.GetAllTypesAsync();
            if (result == null) BadRequest(); //400  
            return Ok(result);
        }
    }
}
