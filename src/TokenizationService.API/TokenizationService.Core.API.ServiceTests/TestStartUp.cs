using Microsoft.Extensions.DependencyInjection;
using TokenizationService.Configuration.Models;
using TokenizationService.Configuration.Repository;
using TokenizationService.Core.API.Repositories;
using TokenizationService.Core.API.ServiceTests.Mocks;

namespace TokenizationService.Core.API.ServiceTests
{
    public class TestStartUp : Startup
    {
        protected override void RegisterRepositories(IServiceCollection services)
        {
            services.AddScoped<IConfigurationRepository<TenantConfiguration>, MockConfigurationRepository>();
            services.AddScoped<ITokenRepository, MockTokenRepository>();
        }
    }
}
