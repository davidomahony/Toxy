using TokenizationService.API.Repositories;
using TokenizationService.Core.API.Models;
using TokenizationService.Core.API.Repositories;

namespace TokenizationService.Core.API.Services
{
    public class EngineService : IEngineService
    {
        private readonly ITokenRepository tokenRepository;
        private readonly ITokenGenerator tokenGenerator;

        public EngineService(ITokenRepository tokenRepository, ITokenGenerator tokenGenerator)
        {
            this.tokenRepository = tokenRepository;
            this.tokenGenerator = tokenGenerator;
        }

        public async Task<TokenizationInformation[]> GenerateTokens(TokenizationInformation[] values)
        {
            // Check if values exist already
            // If so use that

            // If not generate one

        }


        private async Task<TokenizationInformation> GenerateSingleToken(TokenizationInformation value)
        {
            var existingToken = await this.tokenRepository.GetTokenWithValueAsync(value.Value);
            if (existingToken != null)
                return new TokenizationInformation()
                {
                    Value = existingToken.Token,
                    Identifier = value.Identifier
                };



        }


        public Task<DetokenizationInformation[]> FetchTokenValuesAsync(DetokenizationInformation[] tokens)
        {
            throw new NotImplementedException();
        }
    }
}
