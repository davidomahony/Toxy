
namespace TokenizationService.Configuration.Models
{
    public class BaseConfigurationObject
    {
        /// <summary>
        /// A friendy name of the configuration object
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// DIctionary used for chucking information into
        /// </summary>
        public Dictionary<string, string>? Tags { get; set; }

        public DateTime Created { get; set; }

        public bool IsActive { get; set; }
    }
}
