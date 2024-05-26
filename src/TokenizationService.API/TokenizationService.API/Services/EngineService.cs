using TokenizationService.API.Repositories;
using TokenizationService.Core.API.Models;
using TokenizationService.Core.API.Repositories;

namespace TokenizationService.Core.API.Services
{
    public class EngineService : IEngineService
    {
        private readonly ITokenRepository tokenRepository;
        private readonly ITokenServiceGenerator tokenGenerator;
        private readonly IEncryptionService encryptionService;

        public EngineService(
            ITokenRepository tokenRepository, 
            ITokenServiceGenerator tokenGenerator, 
            IEncryptionService encryptionService)
        {
            this.tokenRepository = tokenRepository;
            this.tokenGenerator = tokenGenerator;
            this.encryptionService = encryptionService;
        }

        public async Task<TokenizationInformation[]> GenerateTokens(TokenizationInformation[] values)
        {
            var result = new TokenizationInformation[values.Length];
            for (int i = 0; i < values.Length; i++)
                result[i] = await this.GenerateSingleToken(values[i]);

            return result;
        }

        public async Task<DetokenizationInformation[]> FetchTokenValuesAsync(DetokenizationInformation[] tokens)
        {
            var result = new DetokenizationInformation[tokens.Length];
            for (int i = 0; i < tokens.Length; i++)
                result[i] = await this.GenerateTranslationResult(tokens[i]);

            return result;
        }


        private async Task<TokenizationInformation> GenerateSingleToken(TokenizationInformation value)
        {
            var result = new TokenizationInformation()
            {
                Value = string.Empty,
                Identifier = value.Identifier
            };

            var encryptedValue = this.encryptionService.EncryptString(value.Value, value.Identifier);

            var existingToken = await this.tokenRepository.GetTokenWithValueAsync(encryptedValue);
            if (existingToken != null)
            {
                result.Value = existingToken.Value;
                return result;
            }


            var newToken = await this.tokenGenerator.GenerateNewToken(value.Value, value.Identifier);
            result.Value = newToken;


            // Boom
            await this.tokenRepository.CreateAsync(
                new TokenObject
                {
                    Value = encryptedValue,
                    Token = newToken
                });

            return result;
        }


        private async Task<DetokenizationInformation> GenerateTranslationResult(DetokenizationInformation value)
        {
            var existingToken = await this.tokenRepository.ReadAsync(value.Value);

            var result = new DetokenizationInformation()
            {
                Value = existingToken?.Token ?? string.Empty,
                Identifier = value.Identifier
            };

            return result;
        }
    }
}
