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
        private TenantConfiguration tenantConfiguration;

        private string preWrapper;
        private string postWrapper;

        public TokenParser(IConfigurationRepository<TenantConfiguration> tenantConfigurarionRepo)
        {
            this.tenantConfigurationRepo = tenantConfigurarionRepo;
        }

        public async Task<TokenParserInformation> ParseToken(string token, string clientId)
        {
            await this.FetchWrapper(clientId);

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


        private async Task FetchWrapper(string clientId)
        {
            this.tenantConfiguration = await this.tenantConfigurationRepo.GetConfiguration(clientId);
            if (this.tenantConfiguration == null)
                throw new InvalidOperationException("Unable to locate configuration for tokenization");

            // this is fragile i will fix this later
            this.preWrapper = this.tenantConfiguration.TokenizationInformation.FirstOrDefault().PreWrapper;
            this.postWrapper = this.tenantConfiguration.TokenizationInformation.FirstOrDefault().PostWrapper;
        }
    }
}
