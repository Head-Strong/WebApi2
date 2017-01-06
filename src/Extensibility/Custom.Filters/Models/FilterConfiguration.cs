using Newtonsoft.Json;

namespace Custom.Filters.Models
{
    public class FilterConfiguration
    {
        [JsonProperty("ActionName")]
        public string ActionName { get; set; }

        [JsonProperty("ControllerName")]
        public string ControllerName { get; set; }

        [JsonProperty("Filter")]
        public dynamic Filter { get; set; }

        [JsonProperty("Roles")]
        public string[] Roles { get; set; }        
    }
}
