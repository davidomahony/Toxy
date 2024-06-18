
using MongoDB.Bson.Serialization.Attributes;
using TokenizationService.Configuration.Enums;
using TokenizationService.Enums.Configuration;

namespace TokenizationService.Configuration.Models
{
    [BsonIgnoreExtraElements]
    public class TokenizationConfigurationInformation : BaseConfigurationObject
    {
        public EncryptionType EncryptionType { get; set; }

        public string? Key { get; set; }

        public string? Salt { get; set; }

        public string? DataType { get; set; }

        public string? PreWrapper { get; set; }

        public string? PostWrapper { get; set; }

        public string? PadIdentifier { get; set; }

        public TokenMethodUsed TokenizationMethod { get; set; }
    }
}
