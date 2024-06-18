using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using System.ComponentModel.DataAnnotations;
using TokenizationService.API.Controllers;
using TokenizationService.Configuration.Models;
using TokenizationService.Configuration.Repository;
using TokenizationService.Core.API.Services;
using TokenizationService.Dto.Configuration;

namespace TokenizationService.Core.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ConfigurationController
    {
        private readonly ILogger<ConfigurationController> logger;
        private readonly IConfigurationRepository<TenantConfiguration> repository;
        private readonly IMapper mapper;

        public ConfigurationController(
            ILogger<ConfigurationController> logger,
            IConfigurationRepository<TenantConfiguration> configurationService,
            IMapper mapper)
        {
            this.logger = logger;
            this.repository = configurationService;
            this.mapper = mapper;
        }

        [HttpGet]
        [Route("{id}", Name = nameof(FetchTenantConfiguration))]
        public async Task<ActionResult<TenantConfigurationDto>> FetchTenantConfiguration([Required] string id)
        {
            var result = await this.repository.GetConfigurationAsync(id);
            if (result == null)
                return new NotFoundObjectResult(string.Empty);

            var returned = mapper.Map<TenantConfigurationDto>(result);

            return new OkObjectResult(returned);
        }

        [HttpGet]
        public async Task<ActionResult<TenantConfigurationDto>> FetchAllTenantConfiguration()
        {
            var result = await this.repository.GetAllConfigurations();
            if (result == null)
                return new NotFoundObjectResult(string.Empty);

            var returned = result.Select(mapper.Map<TenantConfigurationDto>).ToList();

            return new OkObjectResult(returned);
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

            if (addTenantConfiguration.TokenRegexInformation != null)
                newConfiguration.TokenRegexInformation = addTenantConfiguration.TokenRegexInformation.Select(itm => new TokenRegexInformation()
                {
                    TokenRegexDetector = itm.TokenRegexDetector,
                    TokenizationMethodUsed = itm.TokenMethodUsed,
                    TokenPartDisector = itm.TokenPartDisector
                });

            var result = await this.repository.AddConfiguration(newConfiguration);

            return new OkObjectResult(result);
        }


        [HttpPut]
        [Route("{id}", Name = nameof(UpdateTenantConfiguration))]
        public async Task<ActionResult<TenantConfigurationDto>> UpdateTenantConfiguration([Required] string id, [Required] AddTenantConfigurationDto modifiedConfiguration)
        {
            var existing = await this.repository.GetConfigurationAsync(id);
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

            if (modifiedConfiguration.TokenRegexInformation != null)
                existing.TokenRegexInformation = modifiedConfiguration.TokenRegexInformation.Select(itm => new TokenRegexInformation()
                {
                    TokenRegexDetector = itm.TokenRegexDetector,
                    TokenizationMethodUsed = itm.TokenMethodUsed,
                    TokenPartDisector = itm.TokenPartDisector
                });

            var result = await this.repository.UpdateConfiguration(id, existing);

            return new OkObjectResult(result);
        }

        private TokenizationConfigurationInformation Translate(TokenizationInformationDto tokenizationInformationDto)
            => new TokenizationConfigurationInformation()
            {
                Name = tokenizationInformationDto.Name,
                EncryptionType = tokenizationInformationDto.TokenizationMethod,
                Salt = tokenizationInformationDto.Salt,
                Key = tokenizationInformationDto.Key,
                DataType = tokenizationInformationDto.DataType,
                PreWrapper = tokenizationInformationDto.PreWrapper,
                PostWrapper = tokenizationInformationDto.PostWrapper,
                PadIdentifier = tokenizationInformationDto.PadIdentifier,
            };

        private ServiceConfigurationInformation Translate(ServiceConfigurationInformationDto serviceConfigurationInformation)
            => new ServiceConfigurationInformation()
            {
                Name = serviceConfigurationInformation.Name,
                AllowedInboundIps = serviceConfigurationInformation.AllowedInboundIps,
                AllowedOutboundIps = serviceConfigurationInformation.AllowedOutboundIps,
            };
    }
}
