using Microsoft.AspNetCore.Mvc;
using TokenizationService.API.Repositories;
using TokenizationService.Core.API.Models;
using TokenizationService.Core.API.Services;

namespace TokenizationService.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TokenizationController : ControllerBase
    {
        private readonly ILogger<TokenizationController> logger;
        private readonly IEngineService engineService;

        public TokenizationController(ILogger<TokenizationController> logger, IEngineService engineService)
        {
            this.engineService = engineService;
            this.logger = logger;
        }

        [HttpGet(Name = nameof(Detokenize))]
        public async Task<ActionResult<DetokenizationResponse>> Detokenize(DetokenizationRequest detokenizationRequest)
        {
            // Validate request
            if (ValidateDetokenizationRequest(detokenizationRequest))
                return BadRequest();

            // Generate tokens
            var tokenResults = await this.engineService.FetchTokenValuesAsync(detokenizationRequest.DetokenizationRequestInformation);

            // Build response
            var response = new DetokenizationResponse()
            {
                DetokenizationResults = tokenResults.ToArray()
            };

            return Ok(response);
        }

        [HttpPost(Name = nameof(Tokenize))]
        public async Task<ActionResult<TokenizationResponse>> Tokenize(TokenizationRequest tokenizationRequest)
        {
            // Validate requests
            if (ValidateTokenizationRequest(tokenizationRequest))
                return BadRequest();

            // Generate tokens
            var tokenResults = await this.engineService.FetchTokensAsync(tokenizationRequest.TokenizationRequestInformation);

            // Build response
            var response = new TokenizationResponse()
            {
                TokenizationResults = tokenResults.ToArray()
            };

            return Ok(response);
        }

        private bool ValidateDetokenizationRequest(DetokenizationRequest detokenizationInformation)
        {


            return true;
        }

        private bool ValidateTokenizationRequest(TokenizationRequest tokenizationRequest)
        {


            return true;
        }


    }
}