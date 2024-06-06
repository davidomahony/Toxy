using Microsoft.AspNetCore.Hosting;

namespace TokenizationService.Core.API.ServiceTests
{
    public class Tests
    {
        private TestServer testServer;

        [SetUp]
        public void Setup()
        {
            _server = new TestServer(new WebHostBuilder()
                    .UseStartup<Startup>());
            _client = _server.CreateClient();
        }

        [Test]
        public void Test1()
        {
            Assert.Pass();
        }
    }
}