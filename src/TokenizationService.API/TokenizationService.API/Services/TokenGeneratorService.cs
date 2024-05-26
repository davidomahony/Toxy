using TokenizationService.Core.API.Repositories;

namespace TokenizationService.Core.API.Services
{
    public class TokenGeneratorService : ITokenServiceGenerator
    {
        private readonly ITokenRepository tokenRepository;
        private readonly IConfiguration configuration;

        public TokenGeneratorService(ITokenRepository tokenRepository, IConfiguration configuration)
        {
            this.tokenRepository = tokenRepository;
            this.configuration = configuration;
        }

        public string Identifier => throw new NotImplementedException();

        public async Task<string> GenerateNewToken(string valueToTokenize, string tokenType)
        {
            string result = string.Empty;

            // i need to check the tokenization method
            // -- Based on the tokenization method I decide the look and feel of the token

            // need to use the repository to get it

            // i need to see how I set the next token

            var newId = Guid.NewGuid();
            if (!await this.tokenRepository.CheckIdExists(newId, tokenType))
                throw new InvalidOperationException("Id exist lets not make duplicates");

            return result;
        }
    }
}
