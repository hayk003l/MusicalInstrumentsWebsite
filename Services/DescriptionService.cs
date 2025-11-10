using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
