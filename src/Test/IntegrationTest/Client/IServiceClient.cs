using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using IntegrationTest.Response;

namespace IntegrationTest.Client
{
    public interface IServiceClient
    {
        Task<string> GetData(string guidData, string authorization);

        Task<CustomerSaveResponse> SaveCustomerAbstracted(string name, string lastName, IEnumerable<string> pins);

        Task<string> GetData1(string guidData, string authorization);
    }
}