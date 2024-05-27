

using TokenizationService.Core.API.Models;

namespace TokenizationService.Core.API.Services
{
    public interface ITokenServiceGenerator
    {
        string Identifier { get; }

        Task<string> GenerateNewToken(TokenizationInformation tokenizationInformation, string client);
    }
}
