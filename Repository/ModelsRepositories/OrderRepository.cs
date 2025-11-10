using Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Repository.ModelsRepositories
{
    public class OrderRepository : RepositoryBase<Order>, IOrderRepository
    {
        public OrderRepository(RepositoryContext repositoryContext) : base(repositoryContext) { }

        public async Task<Order> GetOrderAsync(Guid id, bool trackingChanges) =>
            await FindByCondition(i => i.Id.Equals(id), trackingChanges).FirstOrDefaultAsync();

        public async Task<Order> GetOrderForUserAsync(Guid userId, Guid id, bool trackingChanges) =>
            await FindByCondition(i => i.Id.Equals(id) && i.UserId.Equals(userId), trackingChanges).FirstOrDefaultAsync();

        public async Task<IEnumerable<Order>> GetOrdersForAdminAsync(bool trackingChanges) =>
            await FindAll(trackingChanges).ToListAsync();

        public async Task<IEnumerable<Order>> GetOrdersForUserAsync(Guid userId, bool trackingChanges) =>
            await FindByCondition(i => i.UserId.Equals(userId), trackingChanges).ToListAsync();

        public void CreateOrder(Order order) => Create(order);
        public void DeleteOrder(Order order) => Delete(order);  
    }
}
