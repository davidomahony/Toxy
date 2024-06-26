﻿using System.Text;
using TokenizationService.Configuration.Models;
using TokenizationService.Configuration.Repository;
using TokenizationService.Core.API.Models;
using TokenizationService.Core.API.Repositories;

namespace TokenizationService.Core.API.Services
{
    public class TokenGeneratorService : ITokenServiceGenerator
    {
        private readonly IGenericTokenRepository tokenRepository;
        private readonly IConfiguration configuration;
        private readonly IConfigurationRepository<TenantConfiguration> tenantConfiguration;

        public TokenGeneratorService(IGenericTokenRepository tokenRepository, IConfiguration configuration, IConfigurationRepository<TenantConfiguration> tenantConfiguration)
        {
            this.tokenRepository = tokenRepository;
            this.configuration = configuration;
            this.tenantConfiguration = tenantConfiguration;
        }

        public string Identifier => throw new NotImplementedException();

        public async Task<TokenGeneratorInformation> GenerateNewToken(TokenizationInformation tokenizationInformation, TenantConfiguration tenantConfiguration)
        {
            var tokenizationMethod = tenantConfiguration.TokenizationInformation?.FirstOrDefault(itm => itm.Name.Equals(tokenizationInformation.TokenIdentifier, StringComparison.OrdinalIgnoreCase));
            if (tokenizationMethod == null)
                throw new InvalidOperationException("Unable to locate tokenization method");

            var nextValue = await this.tokenRepository.GetNextCount(tokenizationMethod.Name);

            // right now I am lazy, I should really convert the next value to string with letters

            var builder = new StringBuilder();
            builder.Append(tokenizationMethod.PreWrapper);
            builder.Append(tokenizationMethod.PadIdentifier);
            builder.Append(nextValue);
            builder.Append(tokenizationMethod.PostWrapper);

            return new TokenGeneratorInformation()
            {
                TokenCount = nextValue,
                TokenIdentifier = tokenizationMethod.PadIdentifier,
                TokenValue = builder.ToString()
            };
        }
    }
}
