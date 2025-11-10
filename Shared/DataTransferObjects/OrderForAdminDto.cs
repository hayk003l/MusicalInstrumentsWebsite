using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObjects
{
    public class OrderForAdminDto
    {

        public Guid Id { get; set; }           
        public ItemForOrderDto? Item { get; set; }
        public Guid UserId { get; set; }
        public ShippingDetailsDto? ShippingDetails { get; set; }

    }
}
