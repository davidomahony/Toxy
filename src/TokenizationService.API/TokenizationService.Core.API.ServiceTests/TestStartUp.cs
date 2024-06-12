using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using TokenizationService.Configuration.Models;
using TokenizationService.Configuration.Repository;
using TokenizationService.Core.API.Repositories;
using TokenizationService.Core.API.ServiceTests.Mocks;

namespace TokenizationService.Core.API.ServiceTests
{
    public class TestAppStartUp : WebApplicationFactory<Startup>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            // Is be called after the `ConfigureServices` from the Startup
            // which allows you to overwrite the DI with mocked instances
            builder.ConfigureTestServices(services =>
            {
                services.AddScoped<IConfigurationRepository<TenantConfiguration>, MockConfigurationRepository>();
                services.AddScoped<ITokenRepository, MockTokenRepository>();
            });
        }
    }
}
