using Entities.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Description : BaseModel
    {
        public Guid ItemId { get; set; }
        public Item ItemNavigation { get; set; }

        public string? Country { get; set; }

        public string? DescriptionText { get; set; }

    }
}
