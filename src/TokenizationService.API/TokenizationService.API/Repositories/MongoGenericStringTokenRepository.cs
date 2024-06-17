using MongoDB.Bson;
using MongoDB.Driver;
using TokenizationService.API.Repositories;

namespace TokenizationService.Core.API.Repositories
{
    public class MongoGenericStringTokenRepository : IGenericTokenRepository
    {
        private string connectionString = string.Empty;
        private string dataBaseName;

        private MongoClient client;


        public MongoGenericStringTokenRepository(IConfiguration configuration)
        {
            this.connectionString = configuration["MongoConnection"]
                ?? throw new ArgumentNullException("Missing configuration connection string");

            this.dataBaseName = configuration["Token:DatabaseName"]
                ?? throw new ArgumentNullException("Missing configuration database name");

            this.client = new MongoClient(this.connectionString);
        }

        public async Task<TokenObject> CreateAsync(TokenObject token, string tokenType)
        {
            var collection = GenerateCollection(tokenType);

            await collection.InsertOneAsync(token);

            return token;
        }

        public async Task<int> GetNextCount(string tokenType)
        {
            var collection = GenerateCollection(tokenType);

            var filter = Builders<TokenObject>.Filter.Empty;
            var sort = Builders<TokenObject>.Sort.Descending(x => x.Count);

            // this is not so performant
            var options = new FindOptions<TokenObject>
            {
                Sort = sort,
                Limit = 1
            };

            var cursor = await collection.FindAsync(filter, options);
            var result = await cursor.FirstOrDefaultAsync(); // Use FirstOrDefaultAsync to get a single result

            return result?.Count + 1 ?? 1; // If result is null, start from 1
        }

        public async Task<TokenObject?> GetTokenWithValueAsync(string value, string tokenType)
        {
            var collection = GenerateCollection(tokenType);

            var getFilter = Builders<TokenObject>.Filter.Eq(nameof(TokenObject.EncryptedValue), value);
            var document = (await collection.FindAsync(getFilter)).FirstOrDefaultAsync();

            return await document;

        }

        public async Task<TokenObject> ReadAsync(string id, string tokenType)
        {
            var collection = GenerateCollection(tokenType);

            var getFilter = Builders<TokenObject>.Filter.Eq("Token", id);
            var document = (await collection.FindAsync(getFilter)).FirstOrDefaultAsync();

            return await document;

        }

        public async Task<TokenObject> UpdateAsync(string id, TokenObject entity, string tokenType)
        {
            var collection = GenerateCollection(tokenType);

            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            var filter = Builders<TokenObject>.Filter.Eq(u => u.Id, ObjectId.Parse(id));
            var updateDefinition = Builders<TokenObject>.Update
                .Set(u => u.EncryptedValue, entity.EncryptedValue)
                .Set(u => u.Count, entity.Count);

            await collection.UpdateOneAsync(filter, updateDefinition);

            return entity;
        }

        private IMongoCollection<TokenObject> GenerateCollection(string tokenType)
        {
            IMongoDatabase database = client.GetDatabase(dataBaseName);
            var collection = database.GetCollection<TokenObject>(tokenType);

            if (collection == null)
                throw new InvalidOperationException("Unable to perform action without valid connection");

            return collection;
        }
    }
}
