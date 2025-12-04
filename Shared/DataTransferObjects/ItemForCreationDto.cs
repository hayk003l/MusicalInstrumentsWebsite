using Microsoft.AspNetCore.Http;

namespace Shared.DataTransferObjects
{
    public class ItemForCreationDto : ItemForManipulationDto
    {
        public IFormFile ImageFile { get; set; }
    }
}
