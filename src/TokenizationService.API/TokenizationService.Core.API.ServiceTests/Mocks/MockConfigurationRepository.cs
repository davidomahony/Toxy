using MongoDB.Bson;
using TokenizationService.Configuration.Models;
using TokenizationService.Configuration.Repository;

namespace TokenizationService.Core.API.ServiceTests.Mocks
{
    public class MockConfigurationRepository : IConfigurationRepository<TenantConfiguration>
    {
        public static ObjectId IdForGet = ObjectId.GenerateNewId();
        public static ObjectId IdForDelete = ObjectId.GenerateNewId();
        public static ObjectId IdForUpdate = ObjectId.GenerateNewId();
        private List<TenantConfiguration> tenantConfigurations = new List<TenantConfiguration>();

        public MockConfigurationRepository()
        {
            var testGet = new TenantConfiguration()
            {
                Id = IdForGet,
                Name = "TestName",
                Created = DateTime.MaxValue,
                IsActive = true,
                Tags = new Dictionary<string, string>(),
                TokenizationInformation = new List<TokenizationConfigurationInformation>()
                {
                    new TokenizationConfigurationInformation()
                    {
                        Name = "TestOne",
                        Identifier = 1,

                        Key = "Wowzers",
                        Salt = "3",
                        EncryptionType = Enums.Configuration.EncryptionType.RSA,
                        TokenizationMethod = Configuration.Enums.TokenMethodUsed.StandardString,

                        PadIdentifier = "a",
                        PreWrapper = "cat",
                        PostWrapper = "cas",
                        TokenDetectorRegex = "cas",
                        TokenParsingRegex = "cas",
                    }
                },
                ServiceConfigurationInformation = new List<ServiceConfigurationInformation>(),
            };

            this.tenantConfigurations.Add(testGet);
        }

        public Task<TenantConfiguration> AddConfiguration(TenantConfiguration configurationToAdd)
        {
            configurationToAdd.Id = ObjectId.GenerateNewId();

            tenantConfigurations.Add(configurationToAdd);

            return Task.FromResult(configurationToAdd);
        }

        public Task DeleteConfiguration(string id)
        {
            tenantConfigurations = tenantConfigurations.Where(t => t.Id != ObjectId.Parse(id)).ToList();

            return Task.CompletedTask;
        }

        public Task<List<TenantConfiguration>> GetAllConfigurations()
        {
            return Task.FromResult(this.tenantConfigurations);
        }

        public Task<TenantConfiguration> GetConfigurationAsync(string id)
        {
            return Task.FromResult(tenantConfigurations.FirstOrDefault(itm => itm.Id == ObjectId.Parse(id)));
        }

        public Task<TenantConfiguration> UpdateConfiguration(string id, TenantConfiguration configurationToUpdate)
        {
            tenantConfigurations = tenantConfigurations.Where(t => t.Id != ObjectId.Parse(id)).ToList();

            tenantConfigurations.Add(configurationToUpdate);

            return Task.FromResult(configurationToUpdate);
        }
    }
}
