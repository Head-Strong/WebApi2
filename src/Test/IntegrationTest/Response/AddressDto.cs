using Newtonsoft.Json;

namespace IntegrationTest.Response
{
    public class AddressDto
    {
        [JsonProperty("AddressId")]
        public int AddressId { get; set; }

        [JsonProperty("CustomerId")]
        public int CustomerId { get; set; }

        [JsonProperty("Pin")]
        public string Pin { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}