using Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstractions
{
    public interface IProductServices
    {
        // Get All Products
        Task<IEnumerable<ProductResultDto>> GetAllProductsAsync();
        // Get Product By Id
        Task<ProductResultDto?> GetProductByIdAsync(int id);
        // Get All Types
        Task<IEnumerable<TypeResultDto>> GetAllTypesAsync();
        // Get All Brands
        Task<IEnumerable<BrandResultDto>> GetAllBrandsAsync();
    }
}
