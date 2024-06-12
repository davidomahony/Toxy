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
            var request = new HttpRequestMessage(HttpMethod.Get, "/configuration");
            HttpResponseMessage response = await client.SendAsync(request);

            var responseString = await response.Content.ReadAsStringAsync();
        }
    }
}