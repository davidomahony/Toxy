using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json;
using System.Net;
using TokenizationService.Core.API.ServiceTests.Utilities;
using TokenizationService.Dto.Configuration;

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


            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            var resultBody = JsonConvert.DeserializeObject<IEnumerable<TenantConfigurationDto>>(responseString);
            Assert.That(resultBody, Is.Not.Null);
            Assert.Equals(1, resultBody.Count());
        }

        [Test]
        public async Task ConfigurationControllerTests_AddingInvalidConfig()
        {
            var request = new HttpRequestMessage(HttpMethod.Post, "/configuration");
            var requestDto = new AddTenantConfigurationDto()
            {
                IsActive = true,
                Name = "Test",
                TokenizationInformation = new List<TokenizationInformationDto>()
                {
                    new TokenizationInformationDto()
                    {
                        Name = "Test",
                        PadIdentifier = "dave",
                    },
                    new TokenizationInformationDto()
                    {
                        Name = "Test",
                        PadIdentifier = "dave",
                    }
                }
            };

            request.Content = new StringContent(JsonConvert.SerializeObject(requestDto), System.Text.Encoding.UTF32, "application/json");
            
            HttpResponseMessage response = await client.SendAsync(request);

            var responseString = await response.Content.ReadAsStringAsync();


            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            var resultBody = JsonConvert.DeserializeObject<IEnumerable<TenantConfigurationDto>>(responseString);
            Assert.That(resultBody, Is.Not.Null);
            Assert.Equals(1, resultBody.Count());
        }
    }
}
