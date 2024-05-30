using TokenizationService.API.Repositories;
using TokenizationService.Core.API.Models;
using TokenizationService.Core.API.Repositories;

namespace TokenizationService.Core.API.Services
{
    public class EngineService : IEngineService
    {
        private readonly ITokenRepository tokenRepository;
        private readonly ITokenServiceGenerator tokenGenerator;
        private readonly IEncryptionProvider encryptionProvider;
        private readonly ITokenParser tokenParser;

        public EngineService(
            ITokenRepository tokenRepository,
            ITokenServiceGenerator tokenGenerator,
            IEncryptionProvider encryptionProvider,
            ITokenParser tokenParser)
        {
            this.tokenRepository = tokenRepository;
            this.tokenGenerator = tokenGenerator;
            this.encryptionProvider = encryptionProvider;
            this.tokenParser = tokenParser;
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
                result.Value = existingToken.Token;
                return result;
            }

            // i really dont like using tuples remove me later
            var newToken = await this.tokenGenerator.GenerateNewToken(value, clientId);
            result.Value = newToken.TokenValue;

            // Boom
            await this.tokenRepository.CreateAsync(
                new TokenObject
                {
                    Value = encryptedValue,
                    Count = newToken.TokenCount,
                    Token = newToken.TokenValue
                });

            return result;
        }

        private async Task<DetokenizationInformation> GenerateTranslationResult(DetokenizationInformation value, string clientId)
        {
            var result = new DetokenizationInformation()
            {
                Value =  string.Empty,
                Identifier = value.Identifier
            };

            var info = await this.tokenParser.ParseToken(value.Value, clientId);

            // I actually need to detect which method it used from the pad byte
            // I need to have a repository per token type, this should then detect the correct token repository to choose
            // perhaps the tenant config generates terraform?
            var existingToken = await this.tokenRepository.ReadAsync(value.Value);

            if (existingToken != null)
            {
                var clear = await this.encryptionProvider.DecryptString(existingToken.Value, info.TokenIdentifier, clientId);
                result.Value = clear;
            }

            return result;
        }
    }
}
