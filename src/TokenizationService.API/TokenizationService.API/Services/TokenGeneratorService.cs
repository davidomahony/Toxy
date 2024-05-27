using System.Text;
using TokenizationService.API.Repositories;
using TokenizationService.Configuration.Models;
using TokenizationService.Configuration.Repository;
using TokenizationService.Core.API.Models;
using TokenizationService.Core.API.Repositories;

namespace TokenizationService.Core.API.Services
{
    public class TokenGeneratorService : ITokenServiceGenerator
    {
        private readonly IRepository<TokenObject> tokenRepository;
        private readonly IConfiguration configuration;
        private readonly IConfigurationRepository<TenantConfiguration> tenantConfiguration;

        public TokenGeneratorService(IRepository<TokenObject> tokenRepository, IConfiguration configuration, IConfigurationRepository<TenantConfiguration> tenantConfiguration)
        {
            this.tokenRepository = tokenRepository;
            this.configuration = configuration;
            this.tenantConfiguration = tenantConfiguration;
        }

        public string Identifier => throw new NotImplementedException();

        public async Task<string> GenerateNewToken(TokenizationInformation tokenizationInformation, string clientId)
        {
            var config = await this.tenantConfiguration.GetConfiguration(clientId);
            if (config == null)
                throw new InvalidOperationException("Unable to locate configuration for tokenization");

            var tokenizationMethod = config.TokenizationInformation?.FirstOrDefault(itm => itm.Identifier.Equals(tokenizationInformation.Identifier, StringComparison.OrdinalIgnoreCase));
            if (tokenizationMethod == null)
                throw new InvalidOperationException("Unable to locate tokenization method");

            var nextValue = await this.tokenRepository.GetNextCount();

            // right now I am lazy, I should really convert the next value to string with letters

            var builder = new StringBuilder();
            builder.Append(tokenizationMethod.PreWrapper);
            builder.Append(tokenizationMethod.Identifier);
            builder.Append(nextValue);
            builder.Append(tokenizationMethod.PostWrapper);

            return builder.ToString();
        }
    }
}
