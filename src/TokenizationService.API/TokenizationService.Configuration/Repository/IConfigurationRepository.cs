using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TokenizationService.Configuration.Repository
{
    public interface IConfigurationRepository<T>
    {
        Task<T> GetConfiguration(string id);

        Task<T> UpdateConfiguration(string id, T configurationToUpdate);

        Task<T> AddConfiguration(T configurationToAdd);

        Task DeleteConfiguration(string id);
    }
}
