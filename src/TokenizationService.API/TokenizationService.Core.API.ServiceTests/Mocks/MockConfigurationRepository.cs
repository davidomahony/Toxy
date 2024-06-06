using MongoDB.Bson;
using TokenizationService.Configuration.Models;
using TokenizationService.Configuration.Repository;

namespace TokenizationService.Core.API.ServiceTests.Mocks
{
    internal class MockConfigurationRepository : IConfigurationRepository<TenantConfiguration>
    {
        private List<TenantConfiguration> tenantConfigurations = new List<TenantConfiguration>();

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
