﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstractions
{
    public interface IServiceManager
    {
        IProductServices ProductServices { get; }
        IBasketServices BasketServices { get; }
        ICacheService CacheService { get; }
    }
}
