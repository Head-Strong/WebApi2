using System.Collections.Generic;
using System.IO;
using System.Web;
using Newtonsoft.Json;

namespace Custom.Filters.Models
{
    public static class FilterConfigurationReader
    {
        private static List<FilterConfiguration> _filterConfigurations;

        public static IEnumerable<FilterConfiguration> Get()
        {
            if (_filterConfigurations != null) return _filterConfigurations;

            var fileName = HttpRuntime.BinDirectory + @"FilterConfiguration.json";

            // var fileName = "G://Git//WebApi2//src//Test//UnitTesting//FilterConfiguration.json";

            if (!File.Exists(fileName))
            {
                throw new FileNotFoundException(nameof(fileName));
            }

            using (var r = new StreamReader(fileName))
            {
                var json = r.ReadToEnd();
                _filterConfigurations = JsonConvert.DeserializeObject<List<FilterConfiguration>>(json);
            }

            return _filterConfigurations;

        }
    }
}