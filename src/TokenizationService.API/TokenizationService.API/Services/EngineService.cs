using System.Collections.Generic;
using TokenizationService.API.Repositories;
using TokenizationService.Core.API.Models;
using TokenizationService.Core.API.Repositories;

namespace TokenizationService.Core.API.Services
{
    public class EngineService : IEngineService
    {
        private readonly IRepository<TokenObject> tokenRepository;
        private readonly ITokenServiceGenerator tokenGenerator;
        private readonly IEncryptionProvider encryptionProvider;

        public EngineService(
            IRepository<TokenObject> tokenRepository,
            ITokenServiceGenerator tokenGenerator,
            IEncryptionProvider encryptionProvider)
        {
            this.tokenRepository = tokenRepository;
            this.tokenGenerator = tokenGenerator;
            this.encryptionProvider = encryptionProvider;
        }

        public async Task<TokenizationInformation[]> GenerateTokens(TokenizationInformation[] values, string clientId)
        {
            var result = new TokenizationInformation[values.Length];
            for (int i = 0; i < values.Length; i++)
                result[i] = await this.GenerateSingleToken(values[i], clientId);

            return result;
        }

        public async Task<DetokenizationInformation[]> FetchTokenValuesAsync(DetokenizationInformation[] tokens, string clientId)
        {
            var result = new DetokenizationInformation[tokens.Length];
            for (int i = 0; i < tokens.Length; i++)
                result[i] = await this.GenerateTranslationResult(tokens[i], clientId);

            return result;
        }


        private async Task<TokenizationInformation> GenerateSingleToken(TokenizationInformation value, string clientId)
        {
            var result = new TokenizationInformation()
            {
                Value = string.Empty,
                Identifier = value.Identifier
            };

            var encryptedValue = await this.encryptionProvider.EncryptString(value.Value, value.Identifier, clientId);
            var existingToken = await this.tokenRepository.GetTokenWithValueAsync(encryptedValue);
            if (existingToken != null)
            {
                result.Value = existingToken.Value;
                return result;
            }

            var newToken = await this.tokenGenerator.GenerateNewToken(value, clientId);
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


        private async Task<DetokenizationInformation> GenerateTranslationResult(DetokenizationInformation value, string clientId)
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
