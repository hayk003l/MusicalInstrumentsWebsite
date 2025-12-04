using Microsoft.AspNetCore.Http;
using Service.Contracts;
using System.Security.Claims;


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

            try
            {

                var httpContext = _httpContextAccessor.HttpContext;
                if (httpContext == null)
                {
                    throw new UnauthorizedAccessException("No HTTP context available");
                }
                var user = httpContext.User;
                if (user == null || !user.Identity?.IsAuthenticated == true )
                {
                    throw new UnauthorizedAccessException("User is not authenticated");
                }
                var userIdClaim = user.FindFirst(ClaimTypes.NameIdentifier);
                if (userIdClaim == null)
                {
                    throw new UnauthorizedAccessException("User ID not found in token");
                }
                if (!Guid.TryParse(userIdClaim.Value, out Guid userId))
                {
                    throw new UnauthorizedAccessException("Invalid Guid format");
                }

                return userId;
            }
            catch (UnauthorizedAccessException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new UnauthorizedAccessException("Error retrieving user ID", ex);
            }
        }

        public string GetUserUsername()
        {
            try
            {
                var httpContext = _httpContextAccessor.HttpContext;
                if (httpContext == null)
                {
                    throw new UnauthorizedAccessException("No http context available");
                }
                var user = httpContext.User;
                if (user == null || !user.Identity?.IsAuthenticated == true)
                {
                    throw new UnauthorizedAccessException("User is not authenticated");
                }
                var userClaims = user.FindFirst(ClaimTypes.Name);
                if (userClaims == null || string.IsNullOrEmpty(userClaims.Value))
                {
                    throw new UnauthorizedAccessException("Username not found in token");
                }

                return userClaims.Value;
            }
            catch (UnauthorizedAccessException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new UnauthorizedAccessException("Error retrieving user username", ex);
            }

        }

        public string GetUserRole()
        {
            try
            {
                var httpContext = _httpContextAccessor.HttpContext;
                if (httpContext == null)
                {
                    throw new UnauthorizedAccessException("No http context available");
                }
                var user = httpContext.User;
                if (user == null || !user.Identity?.IsAuthenticated == true)
                {
                    throw new UnauthorizedAccessException("User is not authenticated");
                }
                var roleClaim = user.FindFirst(ClaimTypes.Role);
                if (roleClaim == null || string.IsNullOrEmpty(roleClaim.Value))
                {
                    throw new UnauthorizedAccessException("Username not found in token");
                }

                return roleClaim.Value;
            }
            catch (UnauthorizedAccessException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new UnauthorizedAccessException("Error retrieving user username", ex);
            }

        }

        public bool IsAuthenticated()
        {
            try
            {
                return _httpContextAccessor.HttpContext?.User?.Identity?.IsAuthenticated ?? false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool IsInRole(string role)
        {
            try
            {
                return _httpContextAccessor.HttpContext?.User?.IsInRole(role) ?? false;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
