using Service.Contracts;
using AutoMapper;
using Contracts;

namespace Services
{
    public class DescriptionService : IDescriptionService
    {
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;

        public DescriptionService(IRepositoryManager repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
    }
}
