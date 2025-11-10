using Azure.Identity;
using Microsoft.AspNetCore.Http;
using Service.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;


namespace Service
{
    public class UserContextService : IUserContextService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserContextService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        }

        public Guid GetUserId()
        {
            var userIdString = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            if (!Guid.TryParse(userIdString, out Guid userId))
            {
                throw new UnauthorizedAccessException("Invalid Guid format");
            }

            return userId;
        }

        public string GetUserUsername()
        {
            var userUsernameString = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Name).Value;
            if (string.IsNullOrEmpty(userUsernameString))
            {
                throw new UnauthorizedAccessException("User unauthorized");
            }

            return userUsernameString;
        }

        public string GetUserRole()
        {
            var userRole = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Role).Value;
            return userRole;
        }
    }
}
