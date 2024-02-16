using Microsoft.AspNetCore.Mvc;

namespace TokenizationService.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProxyController : ControllerBase
    {

        private readonly ILogger<ProxyController> logger;

        public ProxyController(ILogger<ProxyController> logger)
        {
            this.logger = logger;
        }

        [HttpGet(Name = "Read")]
        public async Task<ActionResult<object>> Detokenize()
        {
            return Ok();
        }

        [HttpPost()]
        public async Task<ActionResult<object>> Tokenize()
        {

        }
    }
}