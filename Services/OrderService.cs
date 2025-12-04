using Service.Contracts;
using Contracts;
using AutoMapper;
using Shared.DataTransferObjects;
using Entities.Models;
using Entities.Exceptions;

namespace Services
{
    public class OrderService : IOrderService
    {
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;

        public OrderService(IRepositoryManager repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<OrderForAdminDto> GetOrderForAdminAsync(Guid id, bool trackingChanges)
        {
            var order = await _repository.OrderRepository.GetOrderAsync(id, trackingChanges);
            if (order is null)
            {
                throw new OrderNotFoundException(id);
            }
            
            var orderDto = _mapper.Map<OrderForAdminDto>(order);
            var item = await _repository.ItemRepository.GetItemAsync(order.ItemId, trackingChanges);

            if (item is null)
            {
                throw new ItemNotFoundException(order.ItemId);
            }

            orderDto.Item = _mapper.Map<ItemForOrderDto>(item);

            var shippingDetails = await _repository.ShippingDetailsRepository.GetShippingDetailsForOrderAsync(order.Id, trackingChanges);
            orderDto.ShippingDetails = _mapper.Map<ShippingDetailsDto>(shippingDetails);

            return orderDto;
        }

        public async Task<OrderForUserDto> GetOrderForUserAsync(Guid userId, Guid id, bool trackingChanges)
        {
            var order = await _repository.OrderRepository.GetOrderForUserAsync(userId, id, trackingChanges);
            if (order is null)
            {
                throw new OrderNotFoundException(id);
            }

            var orderDto = _mapper.Map<OrderForUserDto>(order);
            var item = await _repository.ItemRepository.GetItemAsync(order.ItemId, trackingChanges);

            if (item is null)
            {
                throw new ItemNotFoundException(order.ItemId);
            }

            orderDto.Item = _mapper.Map<ItemForOrderDto>(item);

            var shippingDetails = await _repository.ShippingDetailsRepository.GetShippingDetailsForOrderAsync(order.Id, trackingChanges);
            if (shippingDetails is null)
            {
                throw new ShippingDetailsNotFoundException(orderDto.Id);
            }
            orderDto.ShippingDetails = _mapper.Map<ShippingDetailsDto>(shippingDetails);

            return orderDto;
        }

        public async Task<IEnumerable<OrderForAdminDto>> GetOrdersForAdminAsync(bool trackingChanges)
        {
            var orders = await _repository.OrderRepository.GetOrdersForAdminAsync(trackingChanges);

            if (orders is null)
            {
                throw new OrdersNotFoundException();
            }

            var orderDto = _mapper.Map<IEnumerable<OrderForAdminDto>>(orders);
            foreach(var order in orderDto)
            {
                var item = await _repository.ItemRepository.GetItemAsync(order.Item.Id, trackingChanges);
                if (item is null) 
                {
                    throw new ItemNotFoundException(order.Id);
                }
                order.Item = _mapper.Map<ItemForOrderDto>(item);
            }
            return orderDto;
        }

        public async Task<IEnumerable<OrderForUserDto>> GetOrdersForUserAsync(Guid userId, bool trackingChanges)
        {
            var orders = await _repository.OrderRepository.GetOrdersForUserAsync(userId, trackingChanges);

            if (orders is null)
            {
                throw new OrdersNotFoundException();
            }

            var orderDto = _mapper.Map<IEnumerable<OrderForUserDto>>(orders);

            foreach( var order in orderDto) 
            {
                var item = await _repository.ItemRepository.GetItemAsync(order.Item.Id, trackingChanges);

                if (item is null)
                {
                    throw new ItemNotFoundException(order.Id);
                }

                order.Item = _mapper.Map<ItemForOrderDto>(item);
                var shippingDetails = await _repository.ShippingDetailsRepository.GetShippingDetailsForOrderAsync(order.Id, trackingChanges);

                if (shippingDetails is null)
                {
                    throw new ShippingDetailsNotFoundException(order.Id);
                }

                order.ShippingDetails = _mapper.Map<ShippingDetailsDto>(shippingDetails);
            }
            return orderDto;

        }

        public async Task<OrderForUserDto> CreateOrderAsync(Guid userId, OrderForCreationDto orderDto)
        {
            var order = _mapper.Map<Order>(orderDto);
            order.UserId = userId;
            
            _repository.OrderRepository.CreateOrder(order);
            await _repository.SaveAsync();

            var result = _mapper.Map<OrderForUserDto>(order);
            return result;
        }

        public async Task DeleteOrderAsync(Guid id, bool trackingChanges)
        {
            var order = await _repository.OrderRepository.GetOrderAsync(id, trackingChanges);

            if (order is null)
            {
                throw new OrderNotFoundException(id);
            }

            _repository.OrderRepository.DeleteOrder(order);
            await _repository.SaveAsync();
        }
    }
}
