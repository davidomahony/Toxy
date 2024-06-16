using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TokenizationService.Configuration.Models
{
    [BsonIgnoreExtraElements]
    public class TenantConfiguration : BaseConfigurationObject
    {
        [BsonId]
        public ObjectId Id { get; set; }

        public IEnumerable<ServiceConfigurationInformation>? ServiceConfigurationInformation { get; set; }

        public IEnumerable<TokenizationConfigurationInformation>? TokenizationInformation { get; set; }

        public IEnumerable<TokenRegexInformation>? TokenRegexInformation { get; set; }
    }
}
