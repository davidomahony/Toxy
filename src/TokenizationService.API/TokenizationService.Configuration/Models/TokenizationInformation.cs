
using TokenizationService.Configuration.Enums;
using TokenizationService.Enums.Configuration;

namespace TokenizationService.Configuration.Models
{
    public class TokenizationConfigurationInformation : BaseConfigurationObject
    {
        public EncryptionType TokenizationMethod { get; set; }

        public string? Key { get; set; }

        public string? Salt { get; set; }

        public string? DataType { get; set; }

        public string? PreWrapper { get; set; }

        public string? PostWrapper { get; set; }

        public string Name { get; set; }

        public string PadIdentifier { get; set; }

        public string TokenRegex { get; set; }

        public TokenMethodUsed TokenizationDataType { get; set; }
    }
}
