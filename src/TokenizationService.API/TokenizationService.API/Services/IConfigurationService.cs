using TokenizationService.Configuration.Models;

namespace TokenizationService.Core.API.Services
{
    public interface IConfigurationService
    {
        Task<TenantConfiguration> GetTenantConfigurationById(string id);

        Task<TenantConfiguration> UpdateTenantConfiguration(string id, TenantConfiguration tenantConfiguration);

        Task<TenantConfiguration> CreateTenantConfiguration(TenantConfiguration tenantConfiguration);

        Task DeleteTenantConfiguration(string id);
    }
}
