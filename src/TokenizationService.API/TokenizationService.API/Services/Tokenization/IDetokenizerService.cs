using TokenizationService.Configuration.Models;
using TokenizationService.Core.API.Models;

namespace TokenizationService.Core.API.Services.Tokenization
{
    public interface IDetokenizerService
    {
        Task<DetokenizationInformation[]> Detokenize(
            DetokenizationInformation[] input, 
            TenantConfiguration configuration);
    }
}
