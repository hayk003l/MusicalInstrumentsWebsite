using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Exceptions
{
    public sealed class ItemsNotFoundException : NotFoundException
    {
        public ItemsNotFoundException() : base("Items not found") { }
    }
}
