using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TokenizationService.Configuration.Models
{
    public class TenantConfiguration : BaseConfigurationObject
    {
        public IEnumerable<ServiceConfigurationInformation>? ServiceConfigurationInformation { get; set; }

        public IEnumerable<TokenizationInformation>? TokenizationInformation { get; set; }

        public DateTime Created { get; set; }
    }
}
