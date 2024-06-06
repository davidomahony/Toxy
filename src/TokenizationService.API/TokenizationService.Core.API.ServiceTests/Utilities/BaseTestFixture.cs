using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;

namespace TokenizationService.Core.API.ServiceTests.Utilities
{
    public class BaseTestFixture
    {
        protected TestServer testServer;
        protected HttpClient client;

        [OneTimeSetUp]
        public void SetupForTests()
        {
            this.testServer = new TestServer(new WebHostBuilder().UseStartup<TestStartUp>().ConfigureAppConfiguration((hostingContext, config) =>
            {
                config.SetBasePath(Directory.GetCurrentDirectory());
                config.AddJsonFile("local.settings.json", optional: true, reloadOnChange: true);
            }));

            client = this.testServer.CreateClient();
        }
    }
}
