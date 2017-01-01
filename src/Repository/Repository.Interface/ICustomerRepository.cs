using System.Collections.Generic;
using ORM.Data;

namespace Repository.Interface
{
    public interface ICustomerRepository : IRepository<Customer>
    {
        string GetData();

        IEnumerable<Domains.Customer> GetCustomers();

        Domains.Customer SaveCustomer(Domains.Customer customer);
    }
}
