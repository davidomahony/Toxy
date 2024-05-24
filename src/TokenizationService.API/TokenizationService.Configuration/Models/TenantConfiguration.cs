using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace TokenizationService.Configuration.Models
{
    public class TenantConfiguration : BaseConfigurationObject
    {

        [BsonId]
        public ObjectId Id { get; set; }

        public IEnumerable<ServiceConfigurationInformation>? ServiceConfigurationInformation { get; set; }

        public IEnumerable<TokenizationInformation>? TokenizationInformation { get; set; }

        public DateTime Created { get; set; }
    }
}
