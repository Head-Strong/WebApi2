using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using IntegrationTest.Response;
using Newtonsoft.Json;
using ORM.Data;

namespace IntegrationTest
{
    public static class Program
    {
        private const string SaveUrl = "http://localhost:2967/api/service/SaveCustomer";
        private const string GetUrl = "http://localhost:2967/api/service/GetCustomers";
        private const string ServerGetUrl = "http://localhost:80/api/service/getdata";
        private const string LocalGetUrl = "http://localhost:2967/api/service/getdata";

        private static readonly HttpClient _httpClient = new HttpClient();

        private static void TestPing()
        {
            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            Parallel.For(0, 100,
                i =>
                {
                    //using (var httpClient = new HttpClient())
                    //{
                    var guidData = Guid.NewGuid().ToString();
                    //_httpClient.DefaultRequestHeaders.Add("Guid", guidData);
                    var response = _httpClient.CustomGetAsync("http://localhost:2967/api/service/GetData", x => x.Headers.Add("Guid", guidData)).GetAwaiter().GetResult();
                    var data = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                    Console.WriteLine("Count :" + i + " Other Data :" + data);
                    Console.WriteLine("===============");

                    var result = SaveCustomerAbstracted(guidData, guidData, new List<string> { guidData });
                    if (result.Status == HttpStatusCode.OK)
                    {
                        Console.WriteLine("Saved Cusomer Id" + result.CustomerDto.Id);
                    }
                    else
                    {
                        Console.WriteLine("Error" + result.ErrorDto.ErrorCategory);
                    }

                });



            //for (var i = 1; i <= 10000; i++)
            //{
            //    //Console.Clear();
            //    Console.WriteLine("Ping " + i);
            //    Console.ForegroundColor = i % 2 == 0 ? ConsoleColor.Red : ConsoleColor.Yellow;

            //    //var allCustomers = GetAllCustomers();
            //    //if (allCustomers.Status == HttpStatusCode.OK)
            //    //{
            //    //    Console.WriteLine("Success for Ping" + i);
            //    //    foreach (var allCustomersCustomerDto in allCustomers.CustomerDtos)
            //    //    {
            //    //        Console.WriteLine(allCustomersCustomerDto.ToString());
            //    //    }
            //    //}
            //    //else
            //    //{
            //    //    Console.WriteLine("Error For Ping:" + i);
            //    //    Console.WriteLine(allCustomers.ErrorDto.ToString());
            //    //}

            //    //using (var httpClient = new HttpClient())
            //    //{
            //    _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            //    var response = _httpClient.CustomGetAsync("https://graph.facebook.com/jammuestates/").GetAwaiter().GetResult();
            //    var data = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            //    Console.WriteLine(data);
            //    Console.WriteLine("===============");
            //    //}

            //    Console.WriteLine("Ping Finished " + i);
            //}
        }

        private static Task<HttpResponseMessage> CustomGetAsync(this HttpClient httpClient, string url, Action<HttpRequestMessage> manipulateData)
        {
            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, url);

            manipulateData(httpRequestMessage);

            return httpClient.SendAsync(httpRequestMessage);
        }

        private static Task<HttpResponseMessage> PostAsJsonAsync<T>(this HttpClient httpClient, string url, T value, Action<HttpRequestMessage> manipulateData)
        {
            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, url)
            {
                Content = new StringContent(JsonConvert.SerializeObject(value), Encoding.UTF8, "application/json")
                //Content = new ObjectContent<T>(value, new JsonMediaTypeFormatter(), (MediaTypeHeaderValue)null)
            };

            manipulateData(httpRequestMessage);

            return httpClient.SendAsync(httpRequestMessage);
        }

        public static void Main(string[] args)
        {
            // var retrier = new Retrier<CustomersGetResponse>();
            // var resultResponse = retrier.RetryWithDelay(GetCustomerResponse);

            try
            {
                TestPing();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            Console.ReadLine();


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
                var response = httpClient.GetAsync(GetUrl).GetAwaiter().GetResult();
                var data = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();

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

            //var sp = ServicePointManager.FindServicePoint(new Uri(""));
            //sp.ConnectionLeaseTimeout = 2000;


            SaveCustomerAbstracted(name, lastName, pins);
        }

        private static CustomerSaveResponse SaveCustomerAbstracted(string name, string lastName, IEnumerable<string> pins)
        {
            var customersResponse = new CustomerSaveResponse();
            var guidData = new Guid().ToString();
            var customer = PrepareCustomer(name, lastName, pins);
            var customerData = new StringContent(JsonConvert.SerializeObject(customer), Encoding.UTF8, "application/json");
            //using (var httpClient = new HttpClient())
            //{
            //_httpClient.DefaultRequestHeaders.Clear();
            //_httpClient.Timeout = TimeSpan.FromSeconds(30);
            //_httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var response = _httpClient.PostAsJsonAsync(SaveUrl, customer, x => x.Headers.Add("Guid", guidData)).GetAwaiter().GetResult();
            var data = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();

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
            //}

            return customersResponse;
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
