using TokenizationService.API.Repositories;

namespace TokenizationService.Core.API.Repositories
{
    /// <summary>
    /// More generic interface, to access any collection which will be specific to token types for performance
    /// </summary>
    public interface IGenericTokenRepository
    {
        Task<TokenObject> CreateAsync(TokenObject token, string tokenType);

        Task<TokenObject> ReadAsync(string id, string tokenType);

        Task<TokenObject> UpdateAsync(string id, TokenObject entity, string tokenType);

        Task<int> GetNextCount(string tokenType);

        Task<TokenObject?> GetTokenWithValueAsync(string value, string tokenType);
    }
}
