namespace Shared.DataTransferObjects
{
    public class OrderForCreationDto

    {
        public Guid ItemId { get; set; }
        public ShippingDetailsForCreationDto ShippingDetails { get; set; }
    }
}
