using MongoDB.Bson;
using TokenizationService.API.Repositories;
using TokenizationService.Core.API.Repositories;

namespace TokenizationService.Core.API.ServiceTests.Mocks
{
    public class MockTokenRepository : ITokenRepository
    {
        private  List<TokenObject> tokenObjects = new List<TokenObject>();

        public Task<TokenObject> CreateAsync(TokenObject entity)
        {
            entity.Id = ObjectId.GenerateNewId();

            tokenObjects.Add(entity);

            return Task.FromResult(entity);
        }

        public Task<int> GetNextCount()
        {
            var itm = this.tokenObjects.Max(x => x.Count);

            return Task.FromResult(itm + 1);
        }

        public Task<TokenObject?> GetTokenWithValueAsync(string value)
        {
            return Task.FromResult(this.tokenObjects.FirstOrDefault(itm => itm.EncryptedValue == value));
        }

        public Task<TokenObject> ReadAsync(string id)
        {
            return Task.FromResult(this.tokenObjects.FirstOrDefault(itm => itm.Id == ObjectId.Parse(id)));
        }

        public Task<TokenObject> UpdateAsync(string id, TokenObject entity)
        {
            tokenObjects = tokenObjects.Where(itm => itm.Id != entity.Id).ToList();

            tokenObjects.Add(entity);

            return Task.FromResult(entity);
        }
    }
}
