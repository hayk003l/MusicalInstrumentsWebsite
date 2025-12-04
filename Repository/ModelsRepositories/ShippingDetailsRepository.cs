using Entities.Models;
using Contracts;
using Microsoft.EntityFrameworkCore;

namespace Repository.ModelsRepositories
{
    public class ShippingDetailsRepository : RepositoryBase<ShippingDetails>, IShippingDetailsRepository
    {
        public ShippingDetailsRepository(RepositoryContext repositoryContext) : base(repositoryContext) { }

        public async Task<ShippingDetails> GetShippingDetailsForOrderAsync(Guid orderId, bool trackingChanges) =>
            await FindByCondition(i => i.OrderId.Equals(orderId), trackingChanges).FirstOrDefaultAsync();
    }
}
