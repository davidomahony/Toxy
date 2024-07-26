using TokenizationService.Configuration.Models;

namespace TokenizationService.Core.API.Services.Encryption
{
    public interface IEncryptionProvider
    {
        string EncryptString(string input, string tokenType, TenantConfiguration tenantConfiguration);

        string DecryptString(string input, string tokenTypeIdentifier, TenantConfiguration tenantConfiguration);
    }
}
