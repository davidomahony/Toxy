using MongoDB.Bson;
using MongoDB.Driver;

namespace TokenizationService.API.Repositories
{
    /// <summary>
    /// I want this to be scoped eventually so we have different token types in different repositories, should be more pe
    /// rformant over time
    /// </summary>
    public class MongoTokenRepository : TokenRepository
    {
        private string connectionString = string.Empty;
        private string dataBaseName;
        private string collectionName;

        private IMongoCollection<TokenObject> collection = null;


        public MongoTokenRepository(IConfiguration configuration) : base(configuration)
        {
            this.connectionString = configuration["MongoConnection"]
                ?? throw new ArgumentNullException("Missing configuration connection string");

            this.collectionName = configuration["Token:CollectionName"]
                ?? throw new ArgumentNullException("Missing configuration collection name");

            this.dataBaseName = configuration["Token:DatabaseName"]
                ?? throw new ArgumentNullException("Missing configuration database name");

            this.Configure();
        }

        public override async Task<TokenObject> CreateAsync(TokenObject entity)
        {
            if (this.collection == null)
                throw new InvalidOperationException("Unable to perform action without valid connection");

            await this.collection.InsertOneAsync(entity);

            return entity;
        }

        public override async Task<int> GetNextCount()
        {
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

        public override async Task<TokenObject?> GetTokenWithValueAsync(string value)
        {
            if (this.collection == null)
                throw new InvalidOperationException("Unable to perform action without valid connection");

            var getFilter = Builders<TokenObject>.Filter.Eq("Value", value);
            var document = (await this.collection.FindAsync(getFilter)).FirstOrDefaultAsync();

            return await document;
        }

        public async override Task<TokenObject> ReadAsync(string id)
        {
            if (this.collection == null)
                throw new InvalidOperationException("Unable to perform action without valid connection");

            var getFilter = Builders<TokenObject>.Filter.Eq("Token", id);
            var document = (await this.collection.FindAsync(getFilter)).FirstOrDefaultAsync();

            return await document;
        }

        /// <summary>
        /// Really this should not be used
        /// </summary>
        /// <param name="id"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        /// <exception cref="ArgumentNullException"></exception>
        public override async Task<TokenObject> UpdateAsync(string id, TokenObject entity)
        {
            if (this.collection == null)
                throw new InvalidOperationException("Unable to perform action without valid connection");

            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            var filter = Builders<TokenObject>.Filter.Eq(u => u.Id, ObjectId.Parse(id));
            var updateDefinition = Builders<TokenObject>.Update
                .Set(u => u.Value, entity.Value)
                .Set(u => u.Count, entity.Count);

            await this.collection.UpdateOneAsync(filter, updateDefinition);

            return entity;
        }

        private void Configure()
        {
            var client = new MongoClient(this.connectionString);
            IMongoDatabase database = client.GetDatabase(dataBaseName);
            this.collection = database.GetCollection<TokenObject>(collectionName);
        }
    }
}
