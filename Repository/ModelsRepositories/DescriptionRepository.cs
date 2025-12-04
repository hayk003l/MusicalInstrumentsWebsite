using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
namespace Repository.ModelsRepositories
{
    public class DescriptionRepository : RepositoryBase<Description>, IDescriptionRepository
    {
        public DescriptionRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }
        public async Task<Description> GetDescriptionForItem(Guid itemId, bool trackingChanges) =>
            await FindByCondition(s => s.ItemId.Equals(itemId), trackingChanges).FirstOrDefaultAsync();

        public void CreateDescriptionForItem(Guid itemId, Description description)
        {
            description.ItemId = itemId;
            Create(description);
        }

    
    }
}
