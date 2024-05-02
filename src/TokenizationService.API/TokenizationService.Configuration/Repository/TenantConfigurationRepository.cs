using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using TokenizationService.Configuration.Models;

namespace TokenizationService.Configuration.Repository
{
    public class TenantConfigurationRepository : IConfigurationRepository<TenantConfiguration>
    {
        private readonly string connectionString = string.Empty;
        private const string DataBaseName = "configuration";
        private const string CollectionName = "tenant";

        private IMongoCollection<BsonDocument>? collection = null;

        public TenantConfigurationRepository(IConfiguration configuration)
        {
            this.connectionString = configuration["MongoConnection"] 
                ?? throw new ArgumentNullException("Missing configuration connection string");

            this.Configure();
        }

        public async Task<TenantConfiguration> AddConfiguration(TenantConfiguration configurationToAdd)
        {
            if (this.collection == null)
                throw new InvalidOperationException("Unable to perform action without valid connection");

            await this.collection.InsertOneAsync(configurationToAdd.ToBsonDocument());

            return configurationToAdd;
        }

        public async Task DeleteConfiguration(string id)
        {
            if (this.collection == null)
                throw new InvalidOperationException("Unable to perform action without valid connection");

            var deleteFilter = Builders<BsonDocument>.Filter.Eq("_id", new ObjectId(id));
            await this.collection.DeleteOneAsync(deleteFilter);
        }

        public async Task<TenantConfiguration> GetConfiguration(string id)
        {
            if (this.collection == null)
                throw new InvalidOperationException("Unable to perform action without valid connection");

            var getFilter = Builders<BsonDocument>.Filter.Eq("_id", new ObjectId(id));
            var document = await this.collection.Find(getFilter).FirstOrDefaultAsync();

            if (document == null)
                return null;

            return ConvertToTenantConfig(document);
        }

        public async Task<TenantConfiguration> UpdateConfiguration(string id, TenantConfiguration configurationToUpdate)
        {
            if (this.collection == null)
                throw new InvalidOperationException("Unable to perform action without valid connection");

            if (configurationToUpdate == null)
                throw new ArgumentNullException(nameof(configurationToUpdate));

            var updateFilter = Builders<BsonDocument>.Filter.Eq("_id", new ObjectId(id));
            var update = configurationToUpdate.ToBsonDocument();

            var result = await this.collection.UpdateOneAsync(updateFilter, update);

            return null;
        }


        private void Configure()
        {
            var client = new MongoClient(this.connectionString);
            IMongoDatabase database = client.GetDatabase(DataBaseName);
            this.collection = database.GetCollection<BsonDocument>(CollectionName);
        }

        private TenantConfiguration ConvertToTenantConfig(BsonDocument document)
            => BsonSerializer.Deserialize<TenantConfiguration>(document);
    }
}
