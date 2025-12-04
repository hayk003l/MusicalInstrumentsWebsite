
namespace Shared.DataTransferObjects
{
    public class ItemDto
    {
        public Guid Id {  get; set; } 
        public string Name {  get; set; }
        public decimal Amount { get; set; }

        public string ImageUrl { get; set; }
        public DescriptionDto Description { get; set; }
    }
}
