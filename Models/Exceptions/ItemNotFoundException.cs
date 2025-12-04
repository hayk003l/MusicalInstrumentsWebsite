using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Exceptions
{
    public sealed class ItemNotFoundException : NotFoundException
    {
        public ItemNotFoundException(Guid itemId) : base($"The company with id: {itemId} doesn't exist in the database") { }
    }
}
