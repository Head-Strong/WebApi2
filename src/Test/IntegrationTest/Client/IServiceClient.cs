using System.Collections.Generic;
using System.Threading.Tasks;
using IntegrationTest.Response;

namespace IntegrationTest.Client
{
    public interface IServiceClient
    {
        Task<string> GetData(string authorization);

        Task<CustomerSaveResponse> SaveCustomerAbstracted(string name, string lastName, IEnumerable<string> pins, string authorization);

        Task<CustomersGetResponse> GetCustomers(string authorization);
    }
}