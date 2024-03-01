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
            var result = new TokenizationInformation[values.Length];
            for (int i = 0; i < values.Length; i++)
                result[i] = await this.GenerateSingleToken(values[i]);

            return result;
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

            var newToken = await this.tokenGenerator.GenerateNewToken(value.Value, value.Identifier);
            await this.tokenRepository.CreateAsync(new TokenObject() { Token = newToken, Value = value.Value });

            return new TokenizationInformation()
            {
                Identifier = value.Identifier,
                Value = newToken,
            };
        }


        public Task<DetokenizationInformation[]> FetchTokenValuesAsync(DetokenizationInformation[] tokens)
        {
            throw new NotImplementedException();
        }

        private async Task<DetokenizationInformation> GenerateResult(DetokenizationInformation value)
        {
            var existingToken = await this.tokenRepository.ReadAsync(value.Value);
            if (existingToken != null)
                return new DetokenizationInformation()
                {
                    Value = existingToken.Token,
                    Identifier = value.Identifier
                };

            return new DetokenizationInformation()
            {
                Identifier = value.Identifier,
                Value = string.Empty,
            };
        }
    }
}
