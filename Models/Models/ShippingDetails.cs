using Entities.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public enum Status
    {
        Pending,
        Shipped,
        Delivered
    }

    public class ShippingDetails : BaseModel
    {
        public Status Status { get; set; } = Status.Pending;
        public string Address { get; set; }
        public Guid OrderId { get; set; }
        public Order OrderNavigation { get; set; }

    }
}
