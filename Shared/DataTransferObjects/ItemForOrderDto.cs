using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObjects
{
    public class ItemForOrderDto
    {
        public Guid Id { get; init; }
        public string Name { get; set; }
        public decimal Amount { get; set; }
        
    }
}
