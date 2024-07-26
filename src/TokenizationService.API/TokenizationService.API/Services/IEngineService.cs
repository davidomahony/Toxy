using TokenizationService.API.Repositories;
using TokenizationService.Core.API.Models;

namespace TokenizationService.Core.API.Services
{
    public interface IEngineService
    {
        Task<TokenizationInformation[]> TokenizeClearValues(TokenizationInformation[] values, string clientId);

        Task<DetokenizationInformation[]> DetokenizeTokenValues(DetokenizationInformation[] tokens, string clientId);
    }
}
