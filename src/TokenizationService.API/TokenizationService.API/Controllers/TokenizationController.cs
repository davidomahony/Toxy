using Microsoft.AspNetCore.Authorization;
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

        [HttpPost]
        [Route("detokenize", Name = nameof(Detokenize))]
        [Authorize()]
        public async Task<ActionResult<DetokenizationResponse>> Detokenize(DetokenizationRequest detokenizationRequest)
        {
            // Validate request
            if (!ValidateDetokenizationRequest(detokenizationRequest))
                return BadRequest();

            // Generate tokens
            var tokenResults = await this.engineService.FetchTokenValuesAsync(detokenizationRequest.DetokenizationRequestInformation, detokenizationRequest.ClientId);

            // Build response
            var response = new DetokenizationResponse()
            {
                DetokenizationResults = tokenResults.ToArray()
            };

            return Ok(response);
        }

        [HttpPost]
        [Route("tokenize", Name = nameof(Tokenize))]
        [Authorize()]
        public async Task<ActionResult<TokenizationResponse>> Tokenize(TokenizationRequest tokenizationRequest)
        {
            // Validate requests
            if (!ValidateTokenizationRequest(tokenizationRequest))
                return BadRequest();

            // Generate tokens
            var tokenResults = await this.engineService.GenerateTokens(tokenizationRequest.TokenizationRequestInformation, tokenizationRequest.ClientId);

            // Build response
            var response = new TokenizationResponse()
            {
                TokenizationResults = tokenResults.ToArray()
            };

            return Ok(response);
        }

        private bool ValidateDetokenizationRequest(DetokenizationRequest detokenizationInformation)
        {
            if (detokenizationInformation == null)
                return false;

            foreach (var tokenInfo in detokenizationInformation.DetokenizationRequestInformation)
            {
                if (string.IsNullOrEmpty(tokenInfo.TokenValue) || string.IsNullOrEmpty(tokenInfo.TokenIdentifier))
                    return false;
            }

            return true;
        }

        private bool ValidateTokenizationRequest(TokenizationRequest tokenizationRequest)
        {
            if (tokenizationRequest == null)
                return false;

            foreach (var tokenInfo in tokenizationRequest.TokenizationRequestInformation)
            {
                if (string.IsNullOrEmpty(tokenInfo.TokenValue) || string.IsNullOrEmpty(tokenInfo.TokenIdentifier))
                    return false;
            }

            return true;
        }


    }
}