
namespace TokenizationService.Configuration.Repository
{
    public interface IConfigurationRepository<T>
    {
        Task<T> GetConfigurationAsync(string id);

        Task<List<T>> GetAllConfigurations();

        Task<T> UpdateConfiguration(string id, T configurationToUpdate);

        Task<T> AddConfiguration(T configurationToAdd);

        Task DeleteConfiguration(string id);
    }
}
