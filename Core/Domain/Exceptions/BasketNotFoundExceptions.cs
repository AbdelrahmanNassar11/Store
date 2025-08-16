using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public class BasketNotFoundExceptions(string id) 
        : NotFoundExceptions($"Basket with Id: {id} Not Found")
    {
    }
}
