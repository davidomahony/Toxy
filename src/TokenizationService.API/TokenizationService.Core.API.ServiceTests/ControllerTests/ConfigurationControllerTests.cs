using Microsoft.AspNetCore.Hosting;
using System.Net;
using TokenizationService.Core.API.ServiceTests.Utilities;

namespace TokenizationService.Core.API.ServiceTests.ControllerTests
{
    public class ConfigurationControllerTests : BaseTestFixture
    {
        [Test]
        public async Task ConfigurationControllerTests_GetAll()
        {
            var response = await client.GetAsync("Configuration");

            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode); 

            var responseString = await response.Content.ReadAsStringAsync();
        }
    }
}