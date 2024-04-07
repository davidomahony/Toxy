using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TokenizationService.Dto.Configuration
{
    public class ServiceConfigurationInformationDto
    {
        /// <summary>
        /// The ID of the configuration object
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// A friendy name of the configuration object
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// DIctionary used for chucking information into
        /// </summary>
        public Dictionary<string, string>? Tags { get; set; }

        public string[]? AllowedInboundIps { get; set; }

        public string[]? AllowedOutboundIps { get; set; }
    }
}
