namespace Shared.DataTransferObjects
{ 
    public class OrderForUserDto
    {
        public Guid Id { get; init; } 
        public ItemForOrderDto Item { get; set; }
        public ShippingDetailsDto ShippingDetails { get; set; }
    }


}
