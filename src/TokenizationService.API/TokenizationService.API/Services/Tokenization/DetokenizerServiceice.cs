using TokenizationService.Configuration.Models;
using TokenizationService.Configuration.Repository;
using TokenizationService.Core.API.Models;
using TokenizationService.Core.API.Repositories;
using TokenizationService.Core.API.Services.Encryption;

namespace TokenizationService.Core.API.Services.Tokenization
{
    public class DetokenizerServiceice : IDetokenizerService
    {
        private readonly IGenericTokenRepository tokenRepository;
        private readonly IEncryptionProvider encryptionProvider;
        private readonly ITokenParser tokenParser;

        public DetokenizerServiceice(IGenericTokenRepository tokenRepository, IEncryptionProvider encryptionProvider, ITokenParser tokenParser)
        {
            this.tokenRepository = tokenRepository;
            this.encryptionProvider = encryptionProvider;
            this.tokenParser = tokenParser;
        }

        public async Task<DetokenizationInformation[]> Detokenize(DetokenizationInformation[] input, TenantConfiguration configuration)
        {
            var result = new DetokenizationInformation[input.Length];

            for (int i = 0; i < input.Length; i++)
                result[i] = await this.GenerateTranslationResultAsync(input[i], configuration);

            return result;
        }

        private async Task<DetokenizationInformation> GenerateTranslationResultAsync(DetokenizationInformation value, TenantConfiguration tenantConfiguration)
        {
            var result = new DetokenizationInformation()
            {
                TokenValue = string.Empty,
                TokenIdentifier = value.TokenIdentifier
            };

            var info = await this.tokenParser.ParseToken(value.TokenValue, value.TokenIdentifier, tenantConfiguration);

            // I actually need to detect which method it used from the pad byte
            // I need to have a repository per token type, this should then detect the correct token repository to choose
            // perhaps the tenant config generates terraform?
            var existingToken = await this.tokenRepository.ReadAsync(value.TokenValue, value.TokenIdentifier);

            if (existingToken != null)
            {
                var clear = this.encryptionProvider.DecryptString(existingToken.EncryptedValue, info.TokenIdentifier, tenantConfiguration);
                result.TokenValue = clear;
            }

            return result;
        }
    }
}
