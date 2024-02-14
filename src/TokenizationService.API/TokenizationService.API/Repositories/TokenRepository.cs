using System.Data.Common;
using System.Data.SqlClient;

namespace TokenizationService.API.Repositories
{
    public class TokenRepository : IRepository<TokenObject>
    {
        private DbConnection dbConnection;

        public TokenRepository(IConfiguration configuration)
        {
            string connectionString = configuration["sqlconnection"] ?? throw new ArgumentNullException(nameof(configuration));
            this.dbConnection = new SqlConnection(connectionString);
        }

        public Task<TokenObject> CreateAsync(TokenObject entity)
        {
            throw new NotImplementedException();
        }

        public Task<TokenObject> ReadAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<TokenObject> UpdateAsync(string id, TokenObject entity)
        {
            throw new NotImplementedException();
        }
    }
}
