using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.DataTransferObjects;
using System.Security.Claims;
using System.Threading.Tasks.Sources;

namespace MusicalInstrumentsStore.Controllers
{
    [ApiController]
    [Authorize]
    [ApiExplorerSettings(GroupName = "v1")]
    [Route("api/orders")]
    public class OrderController : ControllerBase
    {
        private readonly IServiceManager _service;

        public OrderController(IServiceManager service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetOrders()
        {
            var result = await _service.OrderService.GetOrdersForAdminAsync(trackingChanges: false);

            return Ok(result);
        }

        [HttpGet("user")]
        public async Task<IActionResult> GetOrdersForUser()
        {
            Guid userId = _service.UserContextService.GetUserId();
            var result = await _service.OrderService.GetOrdersForUserAsync(userId, trackingChanges: false);
                
            return Ok(result);
        }

        [HttpGet]
        [Route("{Id:guid}")]
        public async Task<IActionResult> GetOrder(Guid id)
        {
            var result = await _service.OrderService.GetOrderForAdminAsync(id, trackingChanges: false);

            return Ok(result);
        }

        [HttpGet("user/{id:guid}", Name = "MyOrders")]
        public async Task<IActionResult> GetOrderForUser(Guid id)
        {
            Guid userId = _service.UserContextService.GetUserId();
            var result = await _service.OrderService.GetOrderForUserAsync(userId, id, trackingChanges: false);

            return Ok(result);
        }

        [HttpPost("user")]
        public async Task<IActionResult> CreateOrder([FromBody] OrderForCreationDto orderForCreating)
        {
            var userId = _service.UserContextService.GetUserId();
            var result = await _service.OrderService.CreateOrderAsync(userId, orderForCreating);

            return CreatedAtRoute("MyOrders", new { id = result.Id }, result);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteOrder(Guid id, bool trackingChanges)
        {
            var order = await _service.OrderService.GetOrderForAdminAsync(id, trackingChanges: false);
            if (order is null)
            {
                return BadRequest("Order doesn't exist");
            }
            await _service.OrderService.DeleteOrderAsync(id, trackingChanges: false);
            return NoContent();
        }
    }
}
 