using System.ComponentModel.DataAnnotations;

namespace Shared.DataTransferObjects
{
    public class DescriptionForManipulationDto
    {
        // [Required(ErrorMessage = "Description Text is a required field")]
        public string DescriptionText { get; set; }

        // [Required(ErrorMessage = "Country is a required field")]
        // [MaxLength(60, ErrorMessage = "Maximum length for Country is 60 characters")]
        public string Country { get; set; }
    }
}
