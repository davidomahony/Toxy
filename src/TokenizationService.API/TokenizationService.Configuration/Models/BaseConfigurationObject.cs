using MongoDB.Bson.Serialization.Attributes;

namespace TokenizationService.Configuration.Models
{
    public class BaseConfigurationObject
    {
        /// <summary>
        /// The ID of the configuration object
        /// </summary>
        [BsonId]
        public Guid Id { get; set; }

        /// <summary>
        /// A friendy name of the configuration object
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// DIctionary used for chucking information into
        /// </summary>
        public Dictionary<string, string>? Tags { get; set; }
    }
}
