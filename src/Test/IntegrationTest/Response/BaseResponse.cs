using System.Net;
using Newtonsoft.Json;

namespace IntegrationTest.Response
{
    public class BaseResponse
    {
        public HttpStatusCode Status { get; set; }

        public ErrorDto ErrorDto { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}