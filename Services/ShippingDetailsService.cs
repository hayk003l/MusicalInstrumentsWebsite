using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Service.Contracts;
using AutoMapper;
using Contracts;
using Shared.DataTransferObjects;

namespace Services
{
    public class ShippingDetailsService : IShippingDetailsService
    {
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;

        public ShippingDetailsService(IRepositoryManager repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ShippingDetailsDto> GetShippingDetails(Guid orderId, bool trackingChanges)
        {
            var shippingDetails = await _repository.ShippingDetailsRepository.GetShippingDetailsForOrderAsync(orderId, trackingChanges);
            if (shippingDetails == null)
            {
                throw new NullReferenceException("ShippingDetails object is null");
            }

            var shippingDetailsDto = _mapper.Map<ShippingDetailsDto>(shippingDetails);
            return shippingDetailsDto;
        }

    }
}
