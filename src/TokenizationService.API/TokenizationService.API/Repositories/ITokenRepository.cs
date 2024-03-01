using TokenizationService.API.Repositories;

namespace TokenizationService.Core.API.Repositories
{
    public interface ITokenRepository : IRepository<TokenObject>
    {
        Task<TokenObject?> GetTokenWithValueAsync(string value);
    }
}
