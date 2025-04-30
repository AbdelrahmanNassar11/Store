using AutoMapper;
using Domain.Contracts;
using Domain.Entities;
using Services.Abstractions;
using Services.Specifications;
using Shared;
using Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ProductServices(IUnitOfWork _unitOfWork, IMapper mapper) : IProductServices
    {
        public async Task<PaginationResponse<ProductResultDto>> GetAllProductsAsync(ProductSpecficationParamters specParams)
        {
            var spec = new ProductWithBrandsAndTypesSpecifications(specParams);

            var products = await _unitOfWork.GetRepository<Product, int>().GetAllAsync(spec);

            var specCount = new ProductWithCountSpecifications(specParams);

            var count = await _unitOfWork.GetRepository<Product, int>().ContAsync(specCount);

            var result = mapper.Map<IEnumerable<ProductResultDto>>(products);

            return new PaginationResponse<ProductResultDto>(specParams.PageIndex,specParams.PageSize,count,result);
        }
       
        public async Task<ProductResultDto?> GetProductByIdAsync(int id)
        {
            var spec = new ProductWithBrandsAndTypesSpecifications(id);
            var product = await _unitOfWork.GetRepository<Product , int>().GetByIdAsync(spec);
            if (product == null)
            {
                throw new Exception("Product not found");
            }
            var result = mapper.Map<ProductResultDto>(product);
            return result;

        }
        public async Task<IEnumerable<BrandResultDto>> GetAllBrandsAsync()
        {
            var brands = await _unitOfWork.GetRepository<ProductBrand, int>().GetAllAsync();
            var result = mapper.Map<IEnumerable<BrandResultDto>>(brands);
            return result;
        }
        public async Task<IEnumerable<TypeResultDto>> GetAllTypesAsync()
        {
            var Types = await _unitOfWork.GetRepository<ProductType, int>().GetAllAsync();
            var result = mapper.Map<IEnumerable<TypeResultDto>>(Types);
            return result;
        }
    }
}
