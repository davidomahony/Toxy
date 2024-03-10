using System.Data.Common;
using System.Data.SqlClient;

namespace TokenizationService.API.Repositories
{
    public class TokenRepository : IRepository<TokenObject>
    {
        private SqlConnection dbConnection;

        // this class needs to be cleaned up its load of shite

        public TokenRepository(IConfiguration configuration)
        {
            string connectionString = configuration["sqlconnection"] 
                ?? throw new ArgumentNullException(nameof(configuration));
            this.dbConnection = new SqlConnection(connectionString);
        }

        public Task<TokenObject> CreateAsync(TokenObject entity)
        {
            var command = this.dbConnection.CreateCommand();
            command.CommandText = $@"
                NSERT INTO example (token, stringvalue) VALUES
                ('*-{entity.Token}-*', '{entity.Value}')";

            var result = command.BeginExecuteNonQuery();

            return null;
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
