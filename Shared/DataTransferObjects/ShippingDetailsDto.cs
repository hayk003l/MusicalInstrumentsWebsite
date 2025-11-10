using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObjects
{
    public enum Status
    {
        Pending,
        Shipped,
        Delivered
    }
    public class ShippingDetailsDto
    {
        public Status Status { get; set; } = Status.Pending;
        public string Address { get; set; }
    }
}
