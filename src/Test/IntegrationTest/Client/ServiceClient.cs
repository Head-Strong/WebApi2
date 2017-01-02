﻿using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using IntegrationTest.Response;
using Newtonsoft.Json;
using ORM.Data;

namespace IntegrationTest.Client
{
    public class ServiceClient : IServiceClient
    {
        private readonly HttpClient _client;
        private const string SaveUrl = "http://localhost:2967/api/service/SaveCustomer";
        private const string GetUrl = "http://localhost:2967/api/service/GetData";

        public ServiceClient(HttpClient client)
        {
            _client = client;
        }

        public Task<string> GetData(string guidData, string authorization)
        {

            var response = _client.CustomGetAsync(GetUrl,
                                                        x => x.Headers.Add("Guid", guidData),
                                                        x => x.Headers.Authorization 
                                                               = new AuthenticationHeaderValue("Bearer", authorization))
                                  .GetAwaiter().GetResult();

            var data = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();

            return Task.FromResult(data);
        }

        public Task<string> GetData1(string guidData, string authorization)
        {

            var response = _client.CustomGetAsync(GetUrl,
                                                        x => x.Headers.Add("Guid", guidData),
                                                        x => x.Headers.Authorization
                                                               = new AuthenticationHeaderValue("Bearer", authorization))
                                  .GetAwaiter().GetResult();

            var data = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();

            return Task.FromResult(data);
        }

        public Task<CustomerSaveResponse> SaveCustomerAbstracted(string name, string lastName, IEnumerable<string> pins)
        {
            var customersResponse = new CustomerSaveResponse();
            var guidData = new Guid().ToString();
            var customer = PrepareCustomer(name, lastName, pins);
            var customerData = new StringContent(JsonConvert.SerializeObject(customer), Encoding.UTF8, "application/json");
            var response = _client.PostAsJsonAsync(SaveUrl, customer, x => x.Headers.Add("Guid", guidData)).GetAwaiter().GetResult();
            var data = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();

            if (response.IsSuccessStatusCode)
            {
                var responseData = JsonConvert.DeserializeObject<CustomerDto>(data, CommonFuncionality.IgnoreNullSettings());
                customersResponse.CustomerDto = responseData;
                customersResponse.Status = response.StatusCode;
                Console.WriteLine("User Saved Successfully");
                Console.WriteLine(customersResponse.CustomerDto.ToString());
            }
            else
            {
                var errorDto = JsonConvert.DeserializeObject<ErrorDto>(data, CommonFuncionality.IgnoreNullSettings());
                customersResponse.ErrorDto = errorDto;
                customersResponse.Status = response.StatusCode;
                Console.WriteLine("Error Occurred While Saving User");
                Console.WriteLine(customersResponse.ErrorDto.ToString());
            }

            return Task.FromResult(customersResponse);
        }

        private static Customer PrepareCustomer(string name, string lastName, IEnumerable<string> pins)
        {
            var customer = new Customer
            {
                Id = 0,
                Name = name,
                LastName = lastName,
                Addresses = new List<Address>()
            };

            foreach (var pin in pins)
            {
                customer.Addresses.Add(new Address
                {
                    AddressId = 0,
                    CustomerId = 0,
                    Pin = pin
                });
            }

            return customer;
        }
    }
}
