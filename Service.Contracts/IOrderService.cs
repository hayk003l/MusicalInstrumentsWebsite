using Shared.DataTransferObjects;

namespace Service.Contracts
{
    public interface IOrderService
    {
        Task<IEnumerable<OrderForUserDto>> GetOrdersForUserAsync(Guid userId, bool trackingChanges);
        Task<IEnumerable<OrderForAdminDto>> GetOrdersForAdminAsync(bool trackingChanges);
        Task<OrderForAdminDto> GetOrderForAdminAsync(Guid id, bool trackingChanges);
        Task<OrderForUserDto> GetOrderForUserAsync(Guid userId, Guid id, bool trackingChanges);
        Task<OrderForUserDto> CreateOrderAsync(Guid userId, OrderForCreationDto orderForCreating);
        Task DeleteOrderAsync(Guid id, bool trackingChanges);
    }

}
