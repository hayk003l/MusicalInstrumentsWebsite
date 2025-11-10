using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Entities.Models;
using Shared.RequestFeatures;

namespace Contracts
{
    public interface IItemRepository
    {
        Task<IEnumerable<Item>> GetItemsAsync(ItemsParameters itemsParameters, bool trackingChanges);

        Task<Item> GetItemAsync(Guid id, bool trackingChanges);
        void CreateItem(Item item);
        void DeleteItem(Item item);
    }
}
