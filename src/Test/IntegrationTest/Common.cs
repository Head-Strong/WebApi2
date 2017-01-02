using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace IntegrationTest
{
    public static class CommonFuncionality
    {
        public static HttpClient GetHttpClient()
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Clear();

            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            return httpClient;
        }

        public static JsonSerializerSettings IgnoreNullSettings()
        {
            return new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            };
        }
    }
}
