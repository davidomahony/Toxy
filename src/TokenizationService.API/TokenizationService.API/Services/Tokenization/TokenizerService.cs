using TokenizationService.API.Repositories;
using TokenizationService.Configuration.Models;
using TokenizationService.Core.API.Models;
using TokenizationService.Core.API.Repositories;
using TokenizationService.Core.API.Services.Encryption;

namespace TokenizationService.Core.API.Services.Tokenization
{
    public class TokenizerService : ITokenizerService
    {
        private readonly IGenericTokenRepository tokenRepository;
        private readonly ITokenServiceGenerator tokenGenerator;
        private readonly IEncryptionProvider encryptionProvider;

        public TokenizerService(IGenericTokenRepository tokenRepository, ITokenServiceGenerator tokenGenerator, IEncryptionProvider encryptionProvider)
        {
            this.tokenRepository = tokenRepository;
            this.tokenGenerator = tokenGenerator;
            this.encryptionProvider = encryptionProvider;
        }

        public async Task<TokenizationInformation[]> Tokenize(TokenizationInformation[] input, TenantConfiguration configuration)
        {
            var result = new TokenizationInformation[input.Length];

            for (int i = 0; i < input.Length; i++)
                result[i] = await this.GenerateSingleToken(input[i], configuration);

            return result;
        }

        private async Task<TokenizationInformation> GenerateSingleToken(TokenizationInformation value, TenantConfiguration tenantConfiguration)
        {
            var result = new TokenizationInformation()
            {
                TokenValue = string.Empty,
                TokenIdentifier = value.TokenIdentifier
            };

            var encryptedValue = this.encryptionProvider.EncryptString(value.TokenValue, value.TokenIdentifier, tenantConfiguration);
            var existingToken = await this.tokenRepository.GetTokenWithValueAsync(encryptedValue, value.TokenIdentifier);
            if (existingToken != null)
            {
                result.TokenValue = existingToken.Token;
                return result;
            }

            // i really dont like using tuples remove me later
            var newToken = await this.tokenGenerator.GenerateNewToken(value, tenantConfiguration);
            result.TokenValue = newToken.TokenValue;

            // Boom
            await this.tokenRepository.CreateAsync(
                new TokenObject
                {
                    EncryptedValue = encryptedValue,
                    Count = newToken.TokenCount, // i am really thinking of swappin gto postgres for this
                    Token = newToken.TokenValue
                }, value.TokenIdentifier);

            return result;
        }
    }
}
