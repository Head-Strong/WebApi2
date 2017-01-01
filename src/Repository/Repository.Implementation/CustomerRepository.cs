using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using ORM.Data;
using Repository.Implementation.AutoMapperConfigMapper;
using Repository.Interface;

namespace Repository.Implementation
{
    public class CustomerRepository : Repository<Customer>, ICustomerRepository
    {
        private readonly TestDatabaseEntities _context;
        private readonly IMapper _autoMapperConfigMapper;

        public CustomerRepository(TestDatabaseEntities context, IAutoMapperConfigMapper autoMapperConfigMapper) : base(context)
        {
            _context = context;
            _autoMapperConfigMapper = autoMapperConfigMapper.ConfigureMap();
        }

        public string GetData()
        {
            return "Success";
        }

        public IEnumerable<Domains.Customer> GetCustomers()
        {
            var customerDb = GetEntities();

            var customers = customerDb.ToList().Select(customerDbValue => _autoMapperConfigMapper.Map<Customer, Domains.Customer>(customerDbValue)).ToList();

            return customers;
        }

        public Domains.Customer SaveCustomer(Domains.Customer customer)
        {
            var customerDb = _autoMapperConfigMapper.Map<Domains.Customer, Customer>(customer);

            _context.Customers.Add(customerDb);

            return _autoMapperConfigMapper.Map<Customer, Domains.Customer>(customerDb);
        }
    }
}
