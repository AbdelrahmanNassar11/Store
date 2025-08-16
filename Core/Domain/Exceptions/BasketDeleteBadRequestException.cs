using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public class BasketDeleteBadRequestException() 
        : BadRequestExceptions("Invalid Operation When Delete Basket!")
    {
    }
}
