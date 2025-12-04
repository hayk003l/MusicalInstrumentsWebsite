using System.ComponentModel.DataAnnotations;

namespace Shared.DataTransferObjects
{
    public class ItemForManipulationDto
    {
        // [Required(ErrorMessage = "Name is a required field.")]
        public string Name { get; set; }

        // [Required(ErrorMessage = "Amount is a required field")]
        public decimal Amount { get; set; }

        public DescriptionForCreationDto Description { get; set; }
    }
}
