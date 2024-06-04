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
        private readonly IConfigurationRepository<TenantConfiguration> tenantConfigurationRepo;

        public async Task<TokenParserInformation> ParseToken(string token, string tokenType, TenantConfiguration tenantConfiguration)
        {

            string pattern = @"(-)(\w{2})(.*?)(-\*)"; // this needs to be loaded from cofig

            var info = new TokenParserInformation();
            Match match = Regex.Match(token, pattern);
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
