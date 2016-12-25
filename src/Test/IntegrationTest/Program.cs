﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
        private const string GetUrl = "http://localhost:2967/api/service/GetCustomers";

        public static void Main(string[] args)
        {
            var retrier = new Retrier<CustomersGetResponse>();
            var resultResponse = retrier.RetryWithDelay(GetCustomerResponse);

            int continueVaribale;
            do
            {
                Console.WriteLine("1. Get Customers");
                Console.WriteLine("2. Get Customer By Id");
                Console.WriteLine("3. Save Customer");

                Console.WriteLine("Select option : -");
                int selectOption;
                int.TryParse(Console.ReadLine(), out selectOption);

                switch (selectOption)
                {
                    case 1:
                        var allCustomers = GetAllCustomers();
                        if (allCustomers.Status == HttpStatusCode.OK)
                        {
                            foreach (var allCustomersCustomerDto in allCustomers.CustomerDtos)
                            {
                                Console.WriteLine(allCustomersCustomerDto.ToString());
                            }
                        }
                        else
                        {
                            Console.WriteLine(allCustomers.ErrorDto.ToString());
                        }

                        break;

                    case 2:
                        GetCustomerById();
                        break;

                    case 3:
                        SaveCustomer();
                        break;
                }

                Console.WriteLine("Do you want to continue [1 (Yes) / 0 (No)] ?");
                var result = Console.ReadLine();
                int.TryParse(result, out continueVaribale);

            } while (continueVaribale == 1);
        }

        private static CustomersGetResponse GetCustomerResponse()
        {
            throw new NotImplementedException();
        }

        private static void GetCustomerById()
        {
            int customerId;
            Console.WriteLine("Enter Customer Id");
            int.TryParse(Console.ReadLine(), out customerId);
            var getCustomers = GetAllCustomers();
            if (getCustomers.Status == HttpStatusCode.OK)
            {
                var customer = getCustomers.CustomerDtos.FirstOrDefault(x => x.Id == customerId);
                Console.WriteLine(customer?.ToString() ?? "Invalid Customer Id");
            }
            else
            {
                Console.WriteLine(getCustomers.ErrorDto.ToString());
            }

        }

        private static CustomersGetResponse GetAllCustomers()
        {
            var customersResponse = new CustomersGetResponse();

            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = httpClient.GetAsync(GetUrl).Result;
                var data = response.Content.ReadAsStringAsync().Result;

                if (response.IsSuccessStatusCode)
                {
                    var responseData = JsonConvert.DeserializeObject<List<CustomerDto>>(data, IgnoreNullSettings());
                    customersResponse.CustomerDtos = responseData;
                    customersResponse.Status = response.StatusCode;
                }
                else
                {
                    var errorDto = JsonConvert.DeserializeObject<ErrorDto>(data, IgnoreNullSettings());
                    customersResponse.ErrorDto = errorDto;
                    customersResponse.Status = response.StatusCode;
                }
            }

            return customersResponse;
        }

        private static void SaveCustomer()
        {
            int noOfPins;
            var pins = new List<string>();
            Console.Clear();
            Console.WriteLine("Enter user Details");
            Console.Write("Firstname :");
            var name = Console.ReadLine();
            Console.WriteLine();
            Console.Write("Lastname :");
            var lastName = Console.ReadLine();
            Console.WriteLine();
            Console.WriteLine("How many no. of pins do you want to enter :-");
            int.TryParse(Console.ReadLine(), out noOfPins);

            for (var i = 0; i < noOfPins; i++)
            {
                Console.WriteLine("Enter Pin Code");
                pins.Add(Console.ReadLine());
            }

            var customersResponse = new CustomerSaveResponse();
            var customer = PrepareCustomer(name, lastName, pins);
            var customerData = new StringContent(JsonConvert.SerializeObject(customer), Encoding.UTF8, "application/json");
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = httpClient.PostAsync(SaveUrl, customerData).Result;
                var data = response.Content.ReadAsStringAsync().Result;

                if (response.IsSuccessStatusCode)
                {
                    var responseData = JsonConvert.DeserializeObject<CustomerDto>(data, IgnoreNullSettings());
                    customersResponse.CustomerDto = responseData;
                    customersResponse.Status = response.StatusCode;
                    Console.WriteLine("User Saved Successfully");
                    Console.WriteLine(customersResponse.CustomerDto.ToString());
                }
                else
                {
                    var errorDto = JsonConvert.DeserializeObject<ErrorDto>(data, IgnoreNullSettings());
                    customersResponse.ErrorDto = errorDto;
                    customersResponse.Status = response.StatusCode;
                    Console.WriteLine("Error Occurred While Saving User");
                    Console.WriteLine(customersResponse.ErrorDto.ToString());
                }
            }
        }

        private static JsonSerializerSettings IgnoreNullSettings()
        {
            return new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            };
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
