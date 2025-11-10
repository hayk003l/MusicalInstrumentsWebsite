using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObjects
{
    public class OrderForCreationDto

    {
        public Guid ItemId { get; set; }
        public ShippingDetailsForCreationDto ShippingDetails { get; set; }
    }
}
