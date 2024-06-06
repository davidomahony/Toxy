using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Driver;
using TokenizationService.Configuration.Models;

namespace TokenizationService.Configuration.Repository
{
    public class TenantConfigurationRepository : IConfigurationRepository<TenantConfiguration>
    {
        private readonly string connectionString = string.Empty;
        private readonly string dataBaseName;
        private readonly string collectionName;

        private IMongoCollection<TenantConfiguration>? collection = null;

        public TenantConfigurationRepository(IConfiguration configuration)
        {
            this.connectionString = configuration["MongoConnection"] 
                ?? throw new ArgumentNullException("Missing configuration connection string");

            this.collectionName = configuration["Configuration:CollectionName"]
                ?? throw new ArgumentNullException("Missing configuration collection name");

            this.dataBaseName = configuration["Configuration:DatabaseName"]
                ?? throw new ArgumentNullException("Missing configuration database name");

            this.Configure();
        }

        public async Task<TenantConfiguration> AddConfiguration(TenantConfiguration configurationToAdd)
        {
            if (this.collection == null)
                throw new InvalidOperationException("Unable to perform action without valid connection");

            await this.collection.InsertOneAsync(configurationToAdd);

            return configurationToAdd;
        }

        public async Task DeleteConfiguration(string id)
        {
            if (this.collection == null)
                throw new InvalidOperationException("Unable to perform action without valid connection");

            var deleteFilter = Builders<TenantConfiguration>.Filter.Eq("_id", new ObjectId(id));
            await this.collection.DeleteOneAsync(deleteFilter);
        }

        public async Task<TenantConfiguration> GetConfigurationAsync(string id)
        {
            if (this.collection == null)
                throw new InvalidOperationException("Unable to perform action without valid connection");

            var getFilter = Builders<TenantConfiguration>.Filter.Eq("_id", new ObjectId(id));
            var document = await this.collection.Find(getFilter).FirstOrDefaultAsync();

            return document;
        }

        public async Task<TenantConfiguration> UpdateConfiguration(string id, TenantConfiguration configurationToUpdate)
        {
            if (this.collection == null)
                throw new InvalidOperationException("Unable to perform action without valid connection");

            if (configurationToUpdate == null)
                throw new ArgumentNullException(nameof(configurationToUpdate));

            var filter = Builders<TenantConfiguration>.Filter.Eq(u => u.Id, ObjectId.Parse(id));
            var updateDefinition = Builders<TenantConfiguration>.Update
                .Set(u => u.Name, configurationToUpdate.Name)
                .Set(u => u.IsActive, configurationToUpdate.IsActive)
                .Set(u => u.TokenizationInformation, configurationToUpdate.TokenizationInformation)
                .Set(u => u.ServiceConfigurationInformation, configurationToUpdate.ServiceConfigurationInformation)
                .Set(u => u.Created, configurationToUpdate.Created);

            await this.collection.UpdateOneAsync(filter, updateDefinition);

            return configurationToUpdate;
        }

        public async Task<List<TenantConfiguration>> GetAllConfigurations()
        {
            if (this.collection == null)
                throw new InvalidOperationException("Unable to perform action without a valid connection");

            // Create an empty filter to match all documents
            var filter = Builders<TenantConfiguration>.Filter.Empty;

            // Retrieve all documents from the collection
            var documents = await this.collection.Find(filter).ToListAsync();

            return documents?.ToList();
        }

        private void Configure()
        {
            var client = new MongoClient(this.connectionString);
            IMongoDatabase database = client.GetDatabase(dataBaseName);
            this.collection = database.GetCollection<TenantConfiguration>(collectionName);
        }
    }
}
