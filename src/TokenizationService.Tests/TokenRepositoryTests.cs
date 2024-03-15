using Microsoft.Extensions.Configuration;
using Moq;
using TokenizationService.API.Repositories;

namespace TokenizationService.Tests
{
    public class TokenRepositoryTests
    {
        private MongoTokenRepository tokenRepository;
        private Mock<IConfiguration> mockConfiguration;


        [SetUp]
        public void Setup()
        {
            this.mockConfiguration = new Mock<IConfiguration>();
            this.mockConfiguration.Setup(x => x["dbName"]).Returns("classname");
            this.mockConfiguration.Setup(x => x["collectionName"]).Returns("propertyname");
            this.mockConfiguration.Setup(x => x["mongo-connection"])
                .Returns("mdummy");
            this.tokenRepository = new MongoTokenRepository(this.mockConfiguration.Object);
            Assert.NotNull(this.tokenRepository);
        }

        [Test]
        public async Task Test1()
        {
            await this.tokenRepository.ReadAsync("65f4afd64beee680e51b6ca6");
        }
    }
}