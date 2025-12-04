using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.DataTransferObjects;
using MusicalInstrumentsStore.ActionFilters;
using Microsoft.AspNetCore.Authorization;
using Shared.RequestFeatures;
using Microsoft.AspNetCore.OData.Query;

namespace MusicalInstrumentsStore.Controllers
{
    [ApiController]
    [ApiExplorerSettings(GroupName = "v1")]
    [Route("api/items")]
    public class ItemController : ControllerBase
    {
        private readonly IServiceManager _service;

        public ItemController(IServiceManager service)
        {
            _service = service;
        }

        [HttpGet]
        [EnableQuery]
        public async Task<IActionResult> GetItems([FromQuery]ItemsParameters itemsParameters)
        {
            var items = await _service.ItemService.GetItemsAsync(itemsParameters, trackingChanges: false);

            return Ok(items);
        }

        [HttpGet]
        [Route("{id:guid}", Name = "ItemById")]
        public async Task<IActionResult> GetItem(Guid id)
        {
            var item = await _service.ItemService.GetItemAsync(id, trackingChanges: false);

            return Ok(item);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> CreateItem([FromForm] ItemForCreationDto itemForCreation)
        {

            var item = await _service.ItemService.CreateItemAsync(itemForCreation);
            return CreatedAtRoute("ItemById", new { Id = item.Id }, item);
        }

        [HttpDelete("{id:guid}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteItem(Guid id)
        {
            var item = await _service.ItemService.GetItemAsync(id, trackingChanges: false);
            if (item is null)
            {
                return BadRequest("Item doesn't exist in database");
            }
            await _service.ItemService.DeleteItemAsync(id, trackingChanges: false);

            return NoContent();
        }

        [HttpPut("{id:guid}")]
        [Authorize(Roles = "Admin")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> UpdateItem(Guid id, [FromBody] ItemForUpdatingDto itemForCreation)
        {   
            await _service.ItemService.UpdateItemAsync(id, itemForCreation, trackingChanges : true);
            return NoContent();
        }

        [HttpPatch("{id:guid}")]
        [Authorize(Roles = "Admin")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> PartiallyUpdateItem(Guid id, [FromBody] JsonPatchDocument<ItemForUpdatingDto> patchDoc)
        {
            if (patchDoc is null)
            {
                return BadRequest("patchDoc object sent from client is null");
            }

            var result = await _service.ItemService.GetItemForPatch(id, trackingChanges: true);
            patchDoc.ApplyTo(result.itemToPatch, ModelState);
            
            TryValidateModel(result.itemToPatch);

            await _service.ItemService.SaveChangesForPatch(result.itemToPatch, result.itemEntity);

            return NoContent();
        }
    }
}
