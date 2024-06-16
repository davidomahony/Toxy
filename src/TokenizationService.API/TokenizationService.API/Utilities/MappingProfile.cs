using AutoMapper;
using TokenizationService.Configuration.Models;
using TokenizationService.Dto.Configuration;

namespace TokenizationService.Core.API.Utilities
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<TenantConfiguration, TenantConfigurationDto>().ReverseMap();
            CreateMap<ServiceConfigurationInformation, ServiceConfigurationInformationDto>().ReverseMap();
            CreateMap<TokenizationConfigurationInformation, TokenizationInformationDto>().ReverseMap();
            CreateMap<TokenRegexInformation, TokenRegexInformationDto>().ReverseMap();
        }
    }
}
