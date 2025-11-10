using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MusicalInstrumentsStore.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/items/{itemId:guid}")]
    public class DescriptionController : ControllerBase
    {
    }
}
