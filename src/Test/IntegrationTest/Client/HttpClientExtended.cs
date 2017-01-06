using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace IntegrationTest.Client
{
    public static class HttpClientExtended
    {
        public static Task<HttpResponseMessage> CustomGetAsync(this HttpClient httpClient, string url, Action<HttpRequestMessage> authorization)
        {
            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, url);

            authorization(httpRequestMessage);

            return httpClient.SendAsync(httpRequestMessage);
        }

        public static Task<HttpResponseMessage> PostAsJsonAsync<T>(this HttpClient httpClient, string url, T value, Action<HttpRequestMessage> authorization)
        {
            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, url)
            {
                Content = new StringContent(JsonConvert.SerializeObject(value), Encoding.UTF8, "application/json")
            };

            authorization(httpRequestMessage);

            return httpClient.SendAsync(httpRequestMessage);
        }
    }
}
