using TokenizationService.API.Repositories;
using TokenizationService.Configuration.Models;
using TokenizationService.Configuration.Repository;
using TokenizationService.Core.API.Models;
using TokenizationService.Core.API.Repositories;
using static MongoDB.Driver.WriteConcern;

namespace TokenizationService.Core.API.Services
{
    public class EngineService : IEngineService
    {
        private readonly ITokenRepository tokenRepository;
        private readonly ITokenServiceGenerator tokenGenerator;
        private readonly IEncryptionProvider encryptionProvider;
        private readonly ITokenParser tokenParser;
        private readonly IConfigurationRepository<TenantConfiguration> tenantConfiguration;

        public EngineService(
            ITokenRepository tokenRepository,
            ITokenServiceGenerator tokenGenerator,
            IEncryptionProvider encryptionProvider,
            ITokenParser tokenParser,
            IConfigurationRepository<TenantConfiguration> tenantConfiguration)
        {
            this.tokenRepository = tokenRepository;
            this.tokenGenerator = tokenGenerator;
            this.encryptionProvider = encryptionProvider;
            this.tokenParser = tokenParser;
            this.tenantConfiguration = tenantConfiguration;
        }

        public async Task<TokenizationInformation[]> GenerateTokens(TokenizationInformation[] values, string clientId)
        {
            var result = new TokenizationInformation[values.Length];
            var config = await this.tenantConfiguration.GetConfigurationAsync(clientId);
            if (config == null)
                throw new InvalidOperationException("Unable to locate configuration for tokenization");

            for (int i = 0; i < values.Length; i++)
                result[i] = await this.GenerateSingleToken(values[i], config);

            return result;
        }

        public async Task<DetokenizationInformation[]> FetchTokenValuesAsync(DetokenizationInformation[] tokens, string clientId)
        {
            var result = new DetokenizationInformation[tokens.Length];
            var config = await this.tenantConfiguration.GetConfigurationAsync(clientId);
            if (config == null)
                throw new InvalidOperationException("Unable to locate configuration for tokenization");

            for (int i = 0; i < tokens.Length; i++)
                result[i] = await this.GenerateTranslationResultAsync(tokens[i], config);

            return result;
        }


        private async Task<TokenizationInformation> GenerateSingleToken(TokenizationInformation value, TenantConfiguration tenantConfiguration)
        {
            var result = new TokenizationInformation()
            {
                ClearValue = string.Empty,
                TokenIdentifier = value.TokenIdentifier
            };

            var encryptedValue = await this.encryptionProvider.EncryptString(value.ClearValue, value.TokenIdentifier, tenantConfiguration);
            var existingToken = await this.tokenRepository.GetTokenWithValueAsync(encryptedValue);
            if (existingToken != null)
            {
                result.ClearValue = existingToken.Token;
                return result;
            }

            // i really dont like using tuples remove me later
            var newToken = await this.tokenGenerator.GenerateNewToken(value, tenantConfiguration);
            result.ClearValue = newToken.TokenValue;

            // Boom
            await this.tokenRepository.CreateAsync(
                new TokenObject
                {
                    EncryptedValue = encryptedValue,
                    Count = newToken.TokenCount,
                    Token = newToken.TokenValue
                });

            return result;
        }

        private async Task<DetokenizationInformation> GenerateTranslationResultAsync(DetokenizationInformation value, TenantConfiguration tenantConfiguration)
        {
            var result = new DetokenizationInformation()
            {
                Token =  string.Empty,
                TokenIdentifier = value.TokenIdentifier
            };

            var info = await this.tokenParser.ParseToken(value.Token, value.TokenIdentifier, tenantConfiguration);

            // I actually need to detect which method it used from the pad byte
            // I need to have a repository per token type, this should then detect the correct token repository to choose
            // perhaps the tenant config generates terraform?
            var existingToken = await this.tokenRepository.ReadAsync(value.Token);

            if (existingToken != null)
            {
                var clear = await this.encryptionProvider.DecryptString(existingToken.EncryptedValue, info.TokenIdentifier, tenantConfiguration);
                result.Token = clear;
            }

            return result;
        }
    }
}
