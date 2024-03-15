
using MongoDB.Bson;
using MongoDB.Driver;

namespace TokenizationService.API.Repositories
{
    public class MongoTokenRepository : TokenRepository
    {
        private readonly string connectionString;
        private readonly string dbName;
        private readonly string collectionName;
        private readonly MongoClient mongoClient;
        private IMongoCollection<TokenObject> tokenCollection;


        public MongoTokenRepository(IConfiguration configuration) : base(configuration)
        {
            this.connectionString = configuration["mongo-connection"] 
                ?? throw new ArgumentNullException(nameof(configuration));
            this.dbName = configuration["dbName"] ?? throw new ArgumentNullException(nameof(this.configuration));
            this.collectionName = configuration["collectionName"] ?? throw new ArgumentNullException(nameof(this.configuration));

            var settings = MongoClientSettings.FromConnectionString(this.connectionString);
            settings.ServerApi = new ServerApi(ServerApiVersion.V1);
            this.mongoClient = new MongoClient(settings);
            var database = mongoClient.GetDatabase(this.dbName);
            this.tokenCollection = database.GetCollection<TokenObject>(this.collectionName);
        }

        public override Task<TokenObject> CreateAsync(TokenObject entity)
        { 
            return null;
        }

        public async override Task<TokenObject> ReadAsync(string id)
        {
            var readValue = await this.tokenCollection.FindAsync(x => x.Id == id);

            return readValue.FirstOrDefault();
        }

        public override Task<TokenObject> UpdateAsync(string id, TokenObject entity)
        {
            throw new NotImplementedException();
        }
    }
}
