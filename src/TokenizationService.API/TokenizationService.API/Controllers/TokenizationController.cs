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
            var result = detokenizationRequest.DetokenizationRequestInformation.Select(
                itm => new DetokenizationInformation()
                {
                    Identifier = itm.Identifier,
                    Value = "clear",
                });


            var dummy = new DetokenizationResponse()
            {
                DetokenizationResults = result.ToArray()
            };

            return Ok(dummy);
        }

        [HttpPost(Name = nameof(Tokenize))]
        public async Task<ActionResult<TokenizationResponse>> Tokenize(TokenizationRequest tokenizationRequest)
        {
            var result = tokenizationRequest.TokenizationRequestInformation.Select(
                itm => new TokenizationInformation()
                {
                    Identifier = itm.Identifier,
                    Value = "token",
                });


            var dummy = new TokenizationResponse()
            {
                TokenizationResults = result.ToArray()
            };

            return Ok(dummy);
        }
    }
}