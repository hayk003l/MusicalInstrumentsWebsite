using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Models;
using Contracts;
using Shared.DataTransferObjects;
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
