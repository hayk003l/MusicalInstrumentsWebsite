namespace Shared.DataTransferObjects
{
    public class ItemForOrderDto
    {
        public Guid Id { get; init; }
        public string Name { get; set; }
        public decimal Amount { get; set; }
        
    }
}
