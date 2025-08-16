using AutoMapper;
using Domain.Contracts;
using Persistencies.Repositories;
using Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ServiceManager
        (
        IUnitOfWork _unitOfWork, 
        IMapper mapper ,
        IBasketRepository basketRepository, 
        ICacheRepository cacheRepository
        ) : IServiceManager
    {
        public IProductServices ProductServices { get; } = new ProductServices(_unitOfWork,mapper);

        public IBasketServices BasketServices { get; } = new BasketServices(basketRepository, mapper);

        public ICacheService CacheService { get; } = new CacheService(cacheRepository);
    }
}
