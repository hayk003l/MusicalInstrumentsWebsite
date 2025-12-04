using System.ComponentModel.DataAnnotations;

namespace Shared.DataTransferObjects
{
    public class DescriptionDto
    {
        [Required(ErrorMessage = "Description Text is a required field")]
        [MaxLength(250, ErrorMessage = "Maximum length for Description Text is 250 characters")]
        public string DescriptionText { get; set; }

        [Required(ErrorMessage = "Country is a required field")]
        [MaxLength(60, ErrorMessage = "Maximum length for Country is 250 characters")]
        public string Country { get; set; }
    }
}
