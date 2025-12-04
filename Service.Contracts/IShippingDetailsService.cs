using Shared.DataTransferObjects;

namespace Service.Contracts
{
    public interface IShippingDetailsService
    {
        Task<ShippingDetailsDto> GetShippingDetails(Guid orderId, bool trackingChanges);
    }
}
