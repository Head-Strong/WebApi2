using System.Collections.Generic;
using Domains;

namespace Service.Interface
{
    public interface IService
    {
        string GetData();

        IEnumerable<Customer> GetCustomers();

        Customer SaveCustomer(Customer customer);

        List<string> GetProfiles();
    }
}
