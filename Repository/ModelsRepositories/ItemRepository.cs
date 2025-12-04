using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Shared.RequestFeatures;
using Repository.ModelsRepositories.Extensions;

namespace Repository.ModelsRepositories
{
    public class ItemRepository : RepositoryBase<Item>, IItemRepository
    {
        public ItemRepository(RepositoryContext repositoryContext) : base(repositoryContext) { }

        public async Task<IEnumerable<Item>> GetItemsAsync(ItemsParameters itemsParameters, bool trackingChanges) =>
            await FindAll(trackingChanges)
            .Filter(itemsParameters.MinPrice, itemsParameters.MaxPrice)
            .Search(itemsParameters.SearchTerm)
            .ToListAsync();

        public async Task<Item> GetItemAsync(Guid id, bool trackingChanges) =>
            await FindByCondition(s => s.Id.Equals(id), trackingChanges).Include(i => i.Description).FirstOrDefaultAsync();

        public void CreateItem(Item item) => Create(item);

        public void DeleteItem(Item item) => Delete(item);

    }
}
