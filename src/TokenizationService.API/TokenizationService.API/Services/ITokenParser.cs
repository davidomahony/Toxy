using TokenizationService.Core.API.Models;

namespace TokenizationService.Core.API.Services
{
    public interface ITokenParser
    {
        Task<TokenParserInformation> ParseToken(string token, string clientId);
    }
}
