using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TokenizationService.Enums.Configuration;

namespace TokenizationService.Configuration.Models
{
    public class TokenizationInformation : BaseConfigurationObject
    {
        public EncryptionType TokenizationMethod { get; set; }
        public string? Key { get; set; }
        public string? Salt { get; set; }
        public string? DataType { get; set; }
        public string? PreWrapper { get; set; }
        public string? PostWrapper { get; set; }
    }
}
