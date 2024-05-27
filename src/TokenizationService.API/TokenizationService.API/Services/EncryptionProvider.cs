using TokenizationService.Configuration.Models;
using TokenizationService.Configuration.Repository;

namespace TokenizationService.Core.API.Services
{
    public class EncryptionProvider : IEncryptionProvider
    {
        private readonly IEnumerable<IEncryptionService> encryptionServices;
        private readonly IConfiguration configuration;
        private readonly IConfigurationRepository<TenantConfiguration> tenantConfiguration;

        public EncryptionProvider(
            IEnumerable<IEncryptionService> encryptionServices, 
            IConfiguration configuration, 
            IConfigurationRepository<TenantConfiguration> tenantConfiguration)
        {
            this.encryptionServices = encryptionServices;
            this.configuration = configuration;
            this.tenantConfiguration = tenantConfiguration;
        }

        public async Task<string> EncryptString(string input, string tokenType, string clientId)
        {
            var config = await this.tenantConfiguration.GetConfiguration(clientId);
            if (config == null)
                throw new InvalidOperationException("Unable to locate configuration for tokenization");

            var tokenizationMethod = config.TokenizationInformation?.FirstOrDefault(itm => itm.Name.Equals(tokenType, StringComparison.OrdinalIgnoreCase));
            if (tokenizationMethod == null)
                throw new InvalidOperationException("Unable to locate tokenization method");

            var service = encryptionServices.FirstOrDefault(itm => itm.Identifier == tokenizationMethod.TokenizationMethod);
            if (service == null)
                throw new InvalidOperationException("Unable to identify correct encryption method");

            return service.EncryptString(input, tokenizationMethod.Key, tokenizationMethod.Salt);
        }
    }
}
