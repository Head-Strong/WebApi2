using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using IntegrationTest.Response;
using Newtonsoft.Json;
using ORM.Data;

namespace IntegrationTest
{
    public static class Program
    {
        private const string SaveUrl = "http://localhost:2967/api/service/SaveCustomer";
        private const string GetUrl = "http://localhost:2968/api/service/GetCustomers1";

        public static void Main(string[] args)
        {
            var customersResponse = new CustomersResponse();
            var customer = GetCustomer();
            var customerData = new StringContent(JsonConvert.SerializeObject(customer), Encoding.UTF8, "application/json");
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //var response = httpClient.PostAsync(SaveUrl, customerData).Result;
                var response = httpClient.GetAsync(GetUrl).Result;

                List<CustomerDto> responseData = null;

                if (response.IsSuccessStatusCode)
                {
                    var data = response.Content.ReadAsStringAsync().Result;
                    responseData = JsonConvert.DeserializeObject<List<CustomerDto>>(data, IgnoreNullSettings());
                    customersResponse.CustomerDtos = responseData;
                    customersResponse.Status = true;
                }
                else
                {
                    var data = response.Content.ReadAsStringAsync().Result;
                    var setData = JsonConvert.DeserializeObject<CustomerDto>(data, IgnoreNullSettings());
                    responseData = new List<CustomerDto>()
                    {
                        setData
                    };

                    customersResponse.CustomerDtos = responseData;
                    customersResponse.Status = false;
                }                
            }

            Console.WriteLine(customersResponse);
            Console.ReadLine();
        }

        private static JsonSerializerSettings IgnoreNullSettings()
        {
            return new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            };
        }

        private static Customer GetCustomer()
        {
            var customer = new Customer
            {
                Id = 0,
                Name = "Salman",
                LastName = "Khan",
                Addresses = new List<Address>
                {
                    new Address
                    {
                        AddressId = 0,
                        CustomerId = 0,
                        Pin = "12345"
                    }
                }
            };

            return customer;
        }

        
    }
}
