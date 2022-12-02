using Microsoft.AspNetCore.Mvc;

namespace ApiProducer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EventProducerController : ControllerBase
    {
        [HttpPost("produce")]
        public ActionResult Publish()
        {
            return Ok();
        }
    }
}
