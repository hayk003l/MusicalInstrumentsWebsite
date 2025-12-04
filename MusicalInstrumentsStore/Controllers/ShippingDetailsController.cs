using Microsoft.AspNetCore.Mvc;

namespace MusicalInstrumentsStore.Controllers
{
    [ApiController]
    [Route("api/orders/{orderId:guid}")]
    public class ShippingDetailsController : ControllerBase
    {
    }
}
