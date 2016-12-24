using System.Collections.Generic;
using Newtonsoft.Json;

namespace IntegrationTest.Response
{
    public class CustomersResponse
    {
        [JsonProperty("CustomerDto")]
        public IEnumerable<CustomerDto> CustomerDtos { get; set; }

        public bool Status { get; set; }
    }
}