
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
        public string Identifier { get; set; }

        public string PadIdentifier { get; set; }
    }
}
