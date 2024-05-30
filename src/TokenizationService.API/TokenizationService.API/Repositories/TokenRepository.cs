using System.Data.Common;
using System.Data.SqlClient;
using TokenizationService.Core.API.Repositories;

namespace TokenizationService.API.Repositories
{
    public abstract class TokenRepository : ITokenRepository
    {
        protected IConfiguration configuration;

        // this class needs to be cleaned up its load of shite

        public TokenRepository(IConfiguration configuration)
        {
            this.configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        public abstract Task<bool> CheckIdExists(Guid id, string tokenType);

        public abstract Task<TokenObject> CreateAsync(TokenObject entity);

        public abstract Task<string> GetNextAvailableToken(string tokenType);

        public abstract Task<int> GetNextCount();

        public abstract Task<TokenObject?> GetTokenWithValueAsync(string value);

        public abstract Task<TokenObject> ReadAsync(string id);

        public abstract Task<TokenObject> UpdateAsync(string id, TokenObject entity);
    }
}
