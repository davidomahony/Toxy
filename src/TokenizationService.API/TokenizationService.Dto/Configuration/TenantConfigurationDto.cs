﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TokenizationService.Dto.Configuration
{
    public class TenantConfigurationDto
    {
        /// <summary>
        /// The ID of the configuration object
        /// </summary>
        [Required]
        public Guid Id { get; set; }

        /// <summary>
        /// A friendy name of the configuration object
        /// </summary>
        [Required]
        public string? Name { get; set; }

        /// <summary>
        /// DIctionary used for chucking information into
        /// </summary>
        public Dictionary<string, string>? Tags { get; set; }

        public IEnumerable<ServiceConfigurationInformationDto>? ServiceConfigurationInformation { get; set; }

        public IEnumerable<TokenizationInformationDto>? TokenizationInformation { get; set; }
    }
}