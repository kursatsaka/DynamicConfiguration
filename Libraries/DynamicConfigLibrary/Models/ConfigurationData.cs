
namespace DynamicConfigLibrary.Models
{
    public class ConfigurationData
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Type { get; set; }

        public string Value { get; set; }

        public string ApplicationName { get; set; }

        public bool IsActive { get; set; }
    }
}
