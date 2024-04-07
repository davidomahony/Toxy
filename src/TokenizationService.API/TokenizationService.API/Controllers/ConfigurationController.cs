using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using TokenizationService.API.Controllers;
using TokenizationService.Configuration.Models;
using TokenizationService.Core.API.Services;
using TokenizationService.Dto.Configuration;

namespace TokenizationService.Core.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ConfigurationController
    {
        private readonly ILogger<ConfigurationController> logger;
        private readonly IConfigurationService configurationService;

        public ConfigurationController(
            ILogger<ConfigurationController> logger, 
            IConfigurationService configurationService)
        {
            this.logger = logger;
            this.configurationService = configurationService;
        }

        [HttpGet(Name = nameof(FetchTenantConfiguration))]
        public async Task<ActionResult<TenantConfigurationDto>> FetchTenantConfiguration([Required] string id)
        {
            var result = await this.configurationService.GetTenantConfigurationById(id);
            if (result == null)
                return new NotFoundObjectResult(string.Empty);

            return new OkObjectResult(result);
        }

        [HttpPost(Name = nameof(AddTenantConfiguration))]
        public async Task<ActionResult<TenantConfigurationDto>> AddTenantConfiguration([Required] AddTenantConfigurationDto addTenantConfiguration)
        {
            var newConfiguration = new TenantConfiguration
            {
                Name = addTenantConfiguration.Name,
                Created = DateTime.UtcNow
            };

            if (addTenantConfiguration.ServiceConfigurationInformation != null)
                newConfiguration.ServiceConfigurationInformation = addTenantConfiguration.ServiceConfigurationInformation
                    .Select(itm => this.Translate(itm));

            if (addTenantConfiguration.TokenizationInformation != null)
                newConfiguration.TokenizationInformation = addTenantConfiguration.TokenizationInformation
                    .Select(itm => this.Translate(itm));

            var result = await this.configurationService.CreateTenantConfiguration(newConfiguration);

            return new OkObjectResult(result);
        }


        [HttpPost(Name = nameof(UpdateTenantConfiguration))]
        public async Task<ActionResult<TenantConfigurationDto>> UpdateTenantConfiguration([Required] string id, [Required] AddTenantConfigurationDto modifiedConfiguration)
        {
            var existing = await this.configurationService.GetTenantConfigurationById(id);
            if (existing == null)
                return new NotFoundObjectResult(string.Empty);

            existing.Name = modifiedConfiguration.Name;

            if (modifiedConfiguration.ServiceConfigurationInformation != null)
                existing.ServiceConfigurationInformation = modifiedConfiguration.ServiceConfigurationInformation
                    .Select(itm => this.Translate(itm));
            else existing.ServiceConfigurationInformation = null;

            if (modifiedConfiguration.TokenizationInformation != null)
                existing.TokenizationInformation = modifiedConfiguration.TokenizationInformation
                    .Select(itm => this.Translate(itm));
            else existing.TokenizationInformation = null;

            var result = await this.configurationService.CreateTenantConfiguration(existing);

            return new OkObjectResult(result);
        }

        private TokenizationInformation Translate(TokenizationInformationDto tokenizationInformationDto)
            => new TokenizationInformation()
            {
                Name = tokenizationInformationDto.Name,
                Id = tokenizationInformationDto.Id,
                TokenizationMethod = tokenizationInformationDto.TokenizationMethod,
                Salt = tokenizationInformationDto.Salt,
                Key = tokenizationInformationDto.Key,
                DataType = tokenizationInformationDto.DataType,
                PreWrapper = tokenizationInformationDto.PreWrapper,
                PostWrapper = tokenizationInformationDto.PostWrapper
            };

        private ServiceConfigurationInformation Translate(ServiceConfigurationInformationDto serviceConfigurationInformation)
            => new ServiceConfigurationInformation()
            {
                Name = serviceConfigurationInformation.Name,
                Id = serviceConfigurationInformation.Id,
                AllowedInboundIps = serviceConfigurationInformation.AllowedInboundIps,
                AllowedOutboundIps = serviceConfigurationInformation.AllowedOutboundIps
            };
    }
}
