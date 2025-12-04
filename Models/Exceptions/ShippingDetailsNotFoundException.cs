using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Exceptions
{
    public sealed class ShippingDetailsNotFoundException : NotFoundException
    {
        public ShippingDetailsNotFoundException(Guid id) : base($"Shipping details for order with id: {id} doesn't exist") { }
    }
}
