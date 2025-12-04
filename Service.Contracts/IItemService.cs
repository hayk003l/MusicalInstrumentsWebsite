using Entities.Models;
using Shared.DataTransferObjects;
using Shared.RequestFeatures;


namespace Service.Contracts
{
    public interface IItemService
    {
        Task<IEnumerable<ItemDto>> GetItemsAsync(ItemsParameters itemsParameters, bool trackingChanges);
        Task<ItemDto> GetItemAsync(Guid id, bool trackingChanges);

        Task<ItemDto> CreateItemAsync(ItemForCreationDto itemForCreation);
        Task DeleteItemAsync(Guid id, bool trackingChanges);
        Task UpdateItemAsync(Guid id, ItemForUpdatingDto itemForUpdating, bool trackingChanges);
        Task<(ItemForUpdatingDto itemToPatch, Item itemEntity)> GetItemForPatch(Guid id, bool trackingChanges);
        Task SaveChangesForPatch(ItemForUpdatingDto itemToPatch, Item itemEntity);

    }
}
