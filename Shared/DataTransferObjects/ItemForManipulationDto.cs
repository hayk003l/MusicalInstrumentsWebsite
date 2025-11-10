using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObjects
{
    public class ItemForManipulationDto
    {
        [Required(ErrorMessage = "Name is a required field.")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Amount is a required field")]
        public decimal? Amount { get; set; }

        public DescriptionForCreationDto? Description { get; set; }
    }
}
