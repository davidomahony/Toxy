using System;

using TokenizationService.Configuration.Enums;

namespace TokenizationService.Dto.Configuration
{
    public class TokenRegexInformationDto
    {
        /// <summary>
        /// What type of token method we are using
        /// </summary>
        public TokenMethodUsed TokenMethodUsed { get; set; }

        /// <summary>
        /// Regex used to detect in large text strings
        /// </summary>
        public string TokenRegexDetector { get; set; }

        /// <summary>
        /// Regex used to split the token to get useful infomration
        /// </summary>
        public string TokenPartDisector { get; set; }
    }
}
