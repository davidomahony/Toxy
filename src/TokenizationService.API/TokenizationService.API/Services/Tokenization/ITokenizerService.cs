using TokenizationService.Configuration.Models;
using TokenizationService.Core.API.Models;

namespace TokenizationService.Core.API.Services.Tokenization
{
    public interface ITokenizerService
    {
        Task<TokenizationInformation[]> Tokenize(
            TokenizationInformation[] inputs, 
            TenantConfiguration configuration);
    }
}
