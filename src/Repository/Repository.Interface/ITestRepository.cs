using System;
using System.Collections.Generic;
using Domains;

namespace Repository.Interface
{
    public interface ITestRepository : IDisposable
    {
        string GetData();

        IEnumerable<Customer> GetCustomers();

        Customer SaveCustomer(Customer customer);
    }
}
