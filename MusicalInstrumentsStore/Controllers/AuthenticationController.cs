using Microsoft.AspNetCore.Mvc;
using MusicalInstrumentsStore.ActionFilters;
using Service.Contracts;
using Shared.DataTransferObjects;

namespace MusicalInstrumentsStore.Controllers
{
    [Route("api/authentication")]
    [ApiExplorerSettings(GroupName = "v1")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IServiceManager _service;

        public AuthenticationController(IServiceManager service) => _service = service;

        [HttpPost]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> RegisterUser([FromBody] UserForRegistrationDto userForRegistration)
        {
            var result = await _service.AuthenticationService.RegisterUser(userForRegistration);

            if (!result.Succeeded)
            {
                foreach(var error in result.Errors)
                {
                    ModelState.TryAddModelError(error.Code, error.Description);
                }
                return BadRequest(ModelState);
            }

            return StatusCode(201);
        }

        [HttpPost("login")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> Authenticate([FromBody] UserForAuthenticationDto userForAuthentication)
        {
            if (!await _service.AuthenticationService.ValidateUser(userForAuthentication))
            {
                return Unauthorized();
            }

            return Ok(new { Token = await _service.AuthenticationService.CreateToken() });
        }

        [HttpGet("info")]
        public IActionResult GetUserInfo()
        {
            var userName = _service.UserContextService.GetUserUsername();
            var userId = _service.UserContextService.GetUserId();
            var userRoles = _service.UserContextService.GetUserRole();

            var userDto = new UserDto
            {
                Username = userName,
                Id = userId,
                Roles = userRoles
            };

            return Ok(userDto);
        }
    }
}
