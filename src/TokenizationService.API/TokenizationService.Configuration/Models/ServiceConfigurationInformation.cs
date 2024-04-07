using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TokenizationService.Configuration.Models
{
    public class ServiceConfigurationInformation : BaseConfigurationObject
    {
        // Potentially need something here around authentication

        public string[]? AllowedInboundIps { get; set; }

        public string[]? AllowedOutboundIps { get; set; }
    }
}
