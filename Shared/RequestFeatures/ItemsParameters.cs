using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.RequestFeatures
{
    public class ItemsParameters : RequestParameters
    {
        public uint MinPrice { get; set; } = 0;
        public uint MaxPrice { get; set; } = uint.MaxValue;

        public bool ValidPriceRange => MinPrice <= MaxPrice;
    }
}
