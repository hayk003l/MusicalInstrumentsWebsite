using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IRepositoryManager
    {
        IItemRepository ItemRepository { get; }
        IOrderRepository OrderRepository { get; }
        IDescriptionRepository DescriptionRepository { get; }
        IShippingDetailsRepository ShippingDetailsRepository { get; }

        Task SaveAsync();

    }
}
