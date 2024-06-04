

using TokenizationService.Configuration.Models;
using TokenizationService.Core.API.Models;

namespace TokenizationService.Core.API.Services
{
    public interface ITokenServiceGenerator
    {
        string Identifier { get; }

        Task<TokenGeneratorInformation> GenerateNewToken(TokenizationInformation tokenizationInformation, TenantConfiguration tenantConfiguration);
    }
}
