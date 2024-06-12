using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;

namespace TokenizationService.Core.API.ServiceTests.Utilities
{
    public class BaseTestFixture
    {
        protected HttpClient client;

        [OneTimeSetUp]
        public void SetupForTests()
        {
            var dp = new TestAppStartUp();

            client = dp.CreateClient();
        }
    }
}
