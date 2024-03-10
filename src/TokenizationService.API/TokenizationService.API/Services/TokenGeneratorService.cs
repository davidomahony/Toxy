using TokenizationService.Core.API.Repositories;

namespace TokenizationService.Core.API.Services
{
    public class TokenGeneratorService : ITokenServiceGenerator
    {
        private readonly ITokenRepository tokenRepository;

        public TokenGeneratorService(ITokenRepository tokenRepository)
        {
            this.tokenRepository = tokenRepository;
        }

        public async Task<string> GenerateNewToken(string valueToTokenize, string tokenType)
        {
            string result = string.Empty;

            var newId = Guid.NewGuid();
            if (!await this.tokenRepository.CheckIdExists(newId, tokenType))
                throw new InvalidOperationException("Id exist lets not make duplicates");

            return result;
        }
    }
}
