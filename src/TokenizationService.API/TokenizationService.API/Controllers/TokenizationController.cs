using Microsoft.AspNetCore.Mvc;
using TokenizationService.Core.API.Models;

namespace TokenizationService.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TokenizationController : ControllerBase
    {

        private readonly ILogger<TokenizationController> logger;

        public TokenizationController(ILogger<TokenizationController> logger)
        {
            this.logger = logger;
        }

        [HttpGet(Name = nameof(Detokenize))]
        public async Task<ActionResult<DetokenizationResponse>> Detokenize(DetokenizationRequest detokenizationRequest)
        {
            return Ok();
        }

        [HttpPost(Name = nameof(Tokenize))]
        public async Task<ActionResult<TokenizationResponse>> Tokenize(TokenizationRequest tokenizationRequest)
        {
            return Ok();
        }
    }
}