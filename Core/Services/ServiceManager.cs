﻿using AutoMapper;
using Domain.Contracts;
using Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ServiceManager(IUnitOfWork _unitOfWork,IMapper mapper) : IServiceManager
    {
        public IProductServices ProductServices { get; } = new ProductServices(_unitOfWork,mapper);
    }
}
