using AutoMapper;
using TokenizationService.Configuration.Models;
using TokenizationService.Dto.Configuration;

namespace TokenizationService.Core.API.Utilities
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<TenantConfiguration, TenantConfigurationDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.ToString()))
                .ReverseMap();
            CreateMap<ServiceConfigurationInformation, ServiceConfigurationInformationDto>().ReverseMap();
            CreateMap<TokenizationInformation, TokenizationInformationDto>().ReverseMap();
        }
    }
}
