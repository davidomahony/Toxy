using System.Data.Common;
using System.Data.SqlClient;

namespace TokenizationService.API.Repositories
{
    public abstract class TokenRepository : IRepository<TokenObject>
    {
        protected IConfiguration configuration;

        // this class needs to be cleaned up its load of shite

        public TokenRepository(IConfiguration configuration)
        {
            this.configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        public abstract Task<TokenObject> CreateAsync(TokenObject entity);

        public abstract Task<int> GetNextCount();

        public abstract Task<TokenObject> ReadAsync(string id);

        public abstract Task<TokenObject> UpdateAsync(string id, TokenObject entity);
    }
}
