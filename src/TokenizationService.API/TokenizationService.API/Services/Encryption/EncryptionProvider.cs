using TokenizationService.Configuration.Models;

namespace TokenizationService.Core.API.Services.Encryption
{
    public class EncryptionProvider : IEncryptionProvider
    {
        private readonly IEnumerable<IEncryptionService> encryptionServices;

        public EncryptionProvider(IEnumerable<IEncryptionService> encryptionServices)
            => this.encryptionServices = encryptionServices;

        public string DecryptString(string input, string tokenTypeIdentifier, TenantConfiguration tenantConfiguration)
        {
            var tokenizationMethod = tenantConfiguration.TokenizationInformation?.FirstOrDefault(itm => itm.PadIdentifier.Equals(tokenTypeIdentifier, StringComparison.OrdinalIgnoreCase));
            if (tokenizationMethod == null)
                throw new InvalidOperationException("Unable to locate tokenization method");

            var service = encryptionServices.FirstOrDefault(itm => itm.Identifier == tokenizationMethod.EncryptionType);
            if (service == null)
                throw new InvalidOperationException("Unable to identify correct encryption method");

            return service.DecryptString(input, tokenizationMethod.Key, tokenizationMethod.Salt);
        }

        public string EncryptString(string input, string tokenType, TenantConfiguration tenantConfiguration)
        {
            var tokenizationMethod = tenantConfiguration.TokenizationInformation?.FirstOrDefault(itm => itm.Name.Equals(tokenType, StringComparison.OrdinalIgnoreCase));
            if (tokenizationMethod == null)
                throw new InvalidOperationException("Unable to locate tokenization method");

            var service = encryptionServices.FirstOrDefault(itm => itm.Identifier == tokenizationMethod.EncryptionType);
            if (service == null)
                throw new InvalidOperationException("Unable to identify correct encryption method");

            return service.EncryptString(input, tokenizationMethod.Key, tokenizationMethod.Salt);
        }
    }
}
