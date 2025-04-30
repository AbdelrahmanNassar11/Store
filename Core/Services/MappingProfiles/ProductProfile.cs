using AutoMapper;
using Domain.Entities;
using Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.MappingProfiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductResultDto>()
                .ForMember(P => P.BrandName, O => O.MapFrom(P => P.ProductBrand.Name))
                .ForMember(p => p.TypeName, O => O.MapFrom(T => T.ProductType.Name))
                .ForMember(p => p.PictureUrl, o => o.MapFrom<PictureResolver>());
            
            //.ForMember(p => p.PictureUrl, o => o.MapFrom(P => $"https://localhost:44377/{P.PictureUrl}"));

            CreateMap<ProductBrand, BrandResultDto>();
            CreateMap<ProductType, TypeResultDto>();
        }
    }
}
