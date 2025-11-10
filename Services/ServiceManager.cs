using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata;
using Service.Contracts;
using Contracts;
using AutoMapper;
using Service;
using Microsoft.AspNetCore.Identity;
using Entities.Models;
using System.Configuration;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http;

namespace Services
{
    public class ServiceManager : IServiceManager
    {
        public readonly Lazy<IItemService> _itemService;
        public readonly Lazy<IOrderService> _orderService;
        public readonly Lazy<IDescriptionService> _descriptionService;
        public readonly Lazy<IShippingDetailsService> _shippingService;
        public readonly Lazy<IAuthenticationService> _autheticationService;
        public readonly Lazy<IUserContextService> _userContextService;
        public ServiceManager (IRepositoryManager repositoryManager, 
            IMapper mapper,
            UserManager<User> userManager,
            IConfiguration configuration,
            RoleManager<IdentityRole<Guid>> roleManager,
            IHttpContextAccessor httpContextAccessor)
        {
            _itemService = new Lazy<IItemService>(() => new ItemService(repositoryManager, mapper));
            _orderService = new Lazy<IOrderService>(() => new OrderService(repositoryManager, mapper)); 
            _descriptionService = new Lazy<IDescriptionService>(() => new DescriptionService(repositoryManager, mapper));
            _shippingService = new Lazy<IShippingDetailsService>(() => new ShippingDetailsService(repositoryManager, mapper));
            _autheticationService = new Lazy<IAuthenticationService>(() => new AuthenticationService(mapper, configuration, userManager, roleManager));
            _userContextService = new Lazy<IUserContextService>(() => new UserContextService(httpContextAccessor));
        }


        public IItemService ItemService => _itemService.Value;
        public IOrderService OrderService => _orderService.Value;
        public IShippingDetailsService ShippingDetailsService => _shippingService.Value;
        public IDescriptionService DescriptionService => _descriptionService.Value;
        public IAuthenticationService AuthenticationService => _autheticationService.Value;
        public IUserContextService UserContextService => _userContextService.Value; 
    }
}
