using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Shared.DataTransferObjects
{ 
    public class OrderForUserDto
    {
        public Guid Id { get; init; } 
        public ItemForOrderDto Item { get; set; }
        public ShippingDetailsDto ShippingDetails { get; set; }
    }


}
