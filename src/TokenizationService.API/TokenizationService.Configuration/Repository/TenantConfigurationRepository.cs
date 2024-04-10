using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TokenizationService.Configuration.Models;

namespace TokenizationService.Configuration.Repository
{
    public class TenantConfigurationRepository : IConfigurationRepository<TenantConfiguration>
    {
        private readonly string connectionString = string.Empty;
        private const string DataBaseName = "Configuration";
        private const string CollectionName = "Tenant";

        public TenantConfigurationRepository()
        {

        }

        private async Task Configure()
        {
            var connectionString = Environment.GetEnvironmentVariable("MONGODB_URI");
            if (connectionString == null)
            {
                Console.WriteLine("You must set your 'MONGODB_URI' environment variable. To learn how to set it, see https://www.mongodb.com/docs/drivers/csharp/current/quick-start/#set-your-connection-string");
                Environment.Exit(0);
            }

            var client = new MongoClient(connectionString);
            IMongoDatabase database = client.GetDatabase(DataBaseName);
            IMongoCollection<BsonDocument> collection = database.GetCollection<BsonDocument>(CollectionName);
        }

        public Task<TenantConfiguration> AddConfiguration(TenantConfiguration configurationToAdd)
        {
            throw new NotImplementedException();
        }

        public Task DeleteConfiguration(string id)
        {
            throw new NotImplementedException();
        }

        public Task<TenantConfiguration> GetConfiguration(string id)
        {
            throw new NotImplementedException();
        }

        public Task<TenantConfiguration> UpdateConfiguration(string id, TenantConfiguration configurationToUpdate)
        {
            throw new NotImplementedException();
        }
    }
}
