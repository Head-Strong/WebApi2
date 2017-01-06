using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using IntegrationTest.Client;
using IntegrationTest.Response;

namespace IntegrationTest
{
    public static class Program
    {
        #region Url Keys
        private const string SaveUrl = "http://localhost:2967/api/service/SaveCustomer";
        private const string GetUrl = "http://localhost:2967/api/service/GetCustomers";
        private const string ServerGetUrl = "http://localhost:80/api/service/getdata";
        private const string LocalGetUrl = "http://localhost:2967/api/service/getdata";
        #endregion

        private static readonly IServiceClient ServiceClient = new ServiceClient(CommonFuncionality.GetHttpClient());

        private static void TestPing()
        {
            Parallel.For(0, 5,
                i =>
                {
                    var authorization = Guid.NewGuid().ToString();
                    var result = ServiceClient.GetData(authorization).Result;
                    Console.WriteLine(result);
                });
        }

        public static void Main(string[] args)
        {
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
            var customersResponse = ServiceClient.GetCustomers(Guid.NewGuid().ToString());
            return customersResponse.Result;
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

            ServiceClient.SaveCustomerAbstracted(name, lastName, pins, Guid.NewGuid().ToString());
        }
    }
}
