
using TokenizationService.Configuration.Enums;

namespace TokenizationService.Configuration.Models
{
    public class TokenRegexInformation
    {
        /// <summary>
        /// What type of token method we are using
        /// </summary>
        public TokenMethodUsed TokenizationMethodUsed { get; set; }

        /// <summary>
        /// Regex used to detect in large text strings
        /// </summary>
        public string? TokenRegexDetector { get; set; }

        /// <summary>
        /// Regex used to split the token to get useful infomration
        /// </summary>
        public string? TokenPartDisector { get; set; }
    }
}
