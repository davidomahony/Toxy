using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TokenizationService.API.Repositories
{
    public class TokenObject
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }


        [BsonRepresentation(BsonType.String)]
        public string Token { get; set; }


        [BsonRepresentation(BsonType.String)]
        public string Value { get; set; }
    }
}
