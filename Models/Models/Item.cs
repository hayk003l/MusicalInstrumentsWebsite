using Entities.Models.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Item : BaseModel
    {

        public string? Name { get; set; }
        public decimal? Amount { get; set; }

        public IEnumerable<Order> Orders { get; set; } = new List<Order>();
        public Description? Description { get; set; }

    }
}
