using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Exceptions
{
    public sealed class DescriptionNotFoundException : NotFoundException
    {
        public DescriptionNotFoundException(Guid id) : base($"Description for item with id: {id} doesn't exist.") { }
    }
}
