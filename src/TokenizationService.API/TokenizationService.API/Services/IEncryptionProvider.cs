using TokenizationService.Configuration.Models;

namespace TokenizationService.Core.API.Services
{
    public interface IEncryptionProvider
    {
        Task<string> EncryptString(string input, string tokenType, TenantConfiguration tenantConfiguration);

        Task<string> DecryptString(string input, string tokenTypeIdentifier, TenantConfiguration tenantConfiguration);
    }
}
