using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace TokenizationService.Configuration.Models
{
    public class TenantConfiguration 
        //: BaseConfigurationObject
    {

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId? Id { get; set; }

        /// <summary>
        /// A friendy name of the configuration object
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// DIctionary used for chucking information into
        /// </summary>
        public Dictionary<string, string>? Tags { get; set; }

        public IEnumerable<ServiceConfigurationInformation>? ServiceConfigurationInformation { get; set; }

        public IEnumerable<TokenizationInformation>? TokenizationInformation { get; set; }

        public DateTime Created { get; set; }

        public bool IsActive { get; set; }
    }
}
