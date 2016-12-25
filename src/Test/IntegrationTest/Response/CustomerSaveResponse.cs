using Newtonsoft.Json;

namespace IntegrationTest.Response
{
    public class CustomerSaveResponse : BaseResponse
    {
        public CustomerDto CustomerDto { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}