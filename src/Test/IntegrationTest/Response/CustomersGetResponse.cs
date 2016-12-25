using System.Collections.Generic;
using Newtonsoft.Json;

namespace IntegrationTest.Response
{
    public class CustomersGetResponse : BaseResponse
    {
        public IEnumerable<CustomerDto> CustomerDtos { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}