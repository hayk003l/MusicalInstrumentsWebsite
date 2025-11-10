using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Models.Base;

namespace Entities.Models
{
    public class Order : BaseModel
    {
        public Guid ItemId { get; set; }
        public Item? ItemNavigation { get; set; }

        public Guid UserId { get; set; }
        public User User { get; set; }
        public ShippingDetails ShippingDetails { get; set; }
    }
}
