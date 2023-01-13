using ApiProducer.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace ApiProducer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EventProducerController : ControllerBase
    {
        private readonly IPublishMessage _publishMessage;

        public EventProducerController(IPublishMessage publishMessage)
        {
            _publishMessage = publishMessage;
        }

        [HttpPost("produce")]
        public ActionResult Publish()
        {
            var response = _publishMessage.Publish("This is my first message from API.");

            return Ok(response);
        }
    }
}
