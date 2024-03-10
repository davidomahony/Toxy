using TokenizationService.API.Repositories;

namespace TokenizationService.Core.API.Repositories
{
    public interface ITokenRepository : IRepository<TokenObject>
    {
        Task<TokenObject?> GetTokenWithValueAsync(string value);

        Task<string> GetNextAvailableToken(string tokenType);

        Task<bool> CheckIdExists(Guid id, string tokenType);
    }
}
