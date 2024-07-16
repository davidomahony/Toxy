
namespace TokenizationService.Configuration.Models
{
    public class BaseConfigurationObject
    {
        /// <summary>
        /// Configuration object, should be unique
        /// </summary>
        public string? Name { get; set; }

        public Dictionary<string, string>? Tags { get; set; }

        public DateTime Created { get; set; }

        public bool IsActive { get; set; }
    }
}
