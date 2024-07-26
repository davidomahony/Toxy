using TokenizationService.Configuration.Models;
using TokenizationService.Configuration.Repository;
using TokenizationService.Core.API.Models;
using TokenizationService.Core.API.Services.Tokenization;

namespace TokenizationService.Core.API.Services
{
    public class EngineService : IEngineService
    {
        private readonly ITokenizerService tokenizationService;
        private readonly IDetokenizerService detokenizationService;
        private readonly IConfigurationRepository<TenantConfiguration> tenantConfiguration;

        public EngineService(ITokenizerService tokenizationService, IDetokenizerService detokenizationService, IConfigurationRepository<TenantConfiguration> tenantConfiguration)
        {
            this.tokenizationService = tokenizationService;
            this.detokenizationService = detokenizationService;
            this.tenantConfiguration = tenantConfiguration;
        }

        public async Task<TokenizationInformation[]> TokenizeClearValues(TokenizationInformation[] values, string clientId)
        {
            var config = await this.tenantConfiguration.GetConfigurationAsync(clientId);
            if (config == null)
                throw new InvalidOperationException("Unable to locate configuration for tokenization");

            return await this.tokenizationService.Tokenize(values, config);
        }

        public async Task<DetokenizationInformation[]> DetokenizeTokenValues(DetokenizationInformation[] tokens, string clientId)
        {
            var config = await this.tenantConfiguration.GetConfigurationAsync(clientId);
            if (config == null)
                throw new InvalidOperationException("Unable to locate configuration for tokenization");

            return await this.detokenizationService.Detokenize(tokens, config);
        }
    }

}
