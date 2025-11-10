using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Models;

namespace Contracts
{
    public interface IOrderRepository
    {
        Task<IEnumerable<Order>> GetOrdersForUserAsync(Guid userId, bool trackingChanges);
        Task<IEnumerable<Order>> GetOrdersForAdminAsync(bool trackingChanges);
        Task<Order> GetOrderForUserAsync(Guid userId, Guid id, bool trackingChanges);
        Task<Order> GetOrderAsync(Guid id, bool trackingChanges);
        void CreateOrder(Order order);
        void DeleteOrder(Order order);
    }
}
