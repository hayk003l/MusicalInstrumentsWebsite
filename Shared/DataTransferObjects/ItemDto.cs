using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObjects
{
    public class ItemDto
    {
        public Guid Id {  get; set; } 
        public string? Name {  get; set; }
        public decimal Amount { get; set; }
        public DescriptionDto Description { get; set; }
    }
}
