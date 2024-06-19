using System.ComponentModel.DataAnnotations;
using TokenizationService.Configuration.Enums;
using TokenizationService.Enums.Configuration;

namespace TokenizationService.Dto.Configuration
{
    public class TokenizationInformationDto
    {
        /// <summary>
        /// A friendy name of the configuration object
        /// </summary>
        [Required]
        public string? Name { get; set; }

        /// <summary>
        /// DIctionary used for chucking information into
        /// </summary>
        public Dictionary<string, string>? Tags { get; set; }

        [Required]
        public EncryptionType EncryptionType { get; set; }

        [Required]
        public TokenMethodUsed TokenizationMethod { get; set; }

        [Required]
        public string? Key { get; set; }

        public string? Salt { get; set; }

        [Required]
        public string? DataType { get; set; }
     
        [Required]
        public string? PreWrapper { get; set; }

        [Required]
        public string? PostWrapper { get; set; }

        [Required]
        public string? PadIdentifier { get; set; }

        public bool IsActive { get; set; }
    }
}
