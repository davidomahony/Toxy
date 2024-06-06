using System.Text.RegularExpressions;
using TokenizationService.API.Repositories;
using TokenizationService.Configuration.Models;
using TokenizationService.Configuration.Repository;
using TokenizationService.Core.API.Models;

namespace TokenizationService.Core.API.Services
{
    /// <summary>
    /// This will lekely need to be done for each token type, date etc
    /// </summary>
    public class TokenParser : ITokenParser
    {
        public async Task<TokenParserInformation> ParseToken(string token, string tokenType, TenantConfiguration tenantConfiguration)
        {
            var tokenizationType = tenantConfiguration.TokenizationInformation.FirstOrDefault(itm => itm.Name.Equals(tokenType, StringComparison.OrdinalIgnoreCase));
            if (tokenizationType == null)
                throw new InvalidOperationException("Unable to locate tokenization method");

            var regexInfo = tenantConfiguration.TokenRegexInformation.FirstOrDefault(itm => itm.TokenMethodUsed == tokenizationType.TokenizationDataType);
            if (regexInfo == null)
                throw new InvalidOperationException("Unable to locate disection pattern");
         //   string pattern = @"(-)(\w{2})(.*?)(-\*)"; // this needs to be loaded from cofig

            var info = new TokenParserInformation();
            Match match = Regex.Match(token, regexInfo.TokenRegexDetector);
            if (match.Success)
            {
                info.PreWrappe =  match.Groups[1].Value;
                info.TokenIdentifier = match.Groups[2].Value;
                info.Value = match.Groups[3].Value;
                info.PostWrapper = match.Groups[4].Value;
            }

            return info;
        }
    }
}
