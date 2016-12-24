using System.Collections.Generic;
using System.Net;

namespace IntegrationTest.Response
{
    public class CustomersResponse
    {
        public IEnumerable<CustomerDto> CustomerDtos { get; set; }

        public HttpStatusCode Status { get; set; }

        public ErrorDto ErrorDto { get; set; }
    }
}