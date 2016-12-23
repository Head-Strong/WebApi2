using Newtonsoft.Json;
using System.Collections.Generic;

namespace IntegrationTest.Response
{
    public class CustomerDto : BaseDto
    {
        [JsonProperty("Id")]
        public int Id { get; set; }

        [JsonProperty("Name")]
        public string Name { get; set; }

        [JsonProperty("LastName")]
        public string LastName { get; set; }

        [JsonProperty("Addresses")]
        public List<AddressDto> Addresses { get; set; }
    }
}