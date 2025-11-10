using Contracts;
using Repository.ModelsRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly Lazy<IItemRepository> _itemRepository;
        private readonly Lazy<IOrderRepository> _orderRepository;
        private readonly Lazy<IDescriptionRepository> _descriptionRepository;
        private readonly Lazy<IShippingDetailsRepository> _shippingDetailsRepository;
        private readonly RepositoryContext _repositoryContext;

        public RepositoryManager (RepositoryContext repositoryContext)
        {
            _repositoryContext = repositoryContext;
            _itemRepository = new Lazy<IItemRepository>(() => new ItemRepository(repositoryContext));
            _orderRepository = new Lazy<IOrderRepository>(() => new OrderRepository(repositoryContext));
            _descriptionRepository = new Lazy<IDescriptionRepository>(() => new DescriptionRepository(repositoryContext));
            _shippingDetailsRepository = new Lazy<IShippingDetailsRepository>(() => new ShippingDetailsRepository(repositoryContext));
        }

        public IItemRepository ItemRepository => _itemRepository.Value;
        public IOrderRepository OrderRepository => _orderRepository.Value;
        public IDescriptionRepository DescriptionRepository => _descriptionRepository.Value;
        public IShippingDetailsRepository ShippingDetailsRepository => _shippingDetailsRepository.Value;

        public async Task SaveAsync() => await _repositoryContext.SaveChangesAsync();

    }
}
