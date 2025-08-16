using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public class BasketBadRequestExceptions() : BadRequestExceptions($"Invalid Operation When Update Or Create Basket")
    {
    }
}
