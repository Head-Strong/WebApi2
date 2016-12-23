using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using AutoMapper;
using ORM.Data;
using Repository.Implementation.AutoMapperConfigMapper;
using Repository.Interface;
using Customer = Domains.Customer;

namespace Repository.Implementation
{
    public class TestRepository : ITestRepository
    {
        private readonly TestDatabaseEntities _testDatabaseEntities;
        private bool _disposed = false;
        private readonly IMapper _mapper;


        public TestRepository(TestDatabaseEntities testDatabaseEntities, IAutoMapperConfigMapper autoMapperConfigMapper)
        {
            _testDatabaseEntities = testDatabaseEntities;
            _mapper = autoMapperConfigMapper.ConfigureMap();
        }

        public string GetData()
        {
            return "Success";
        }

        public IEnumerable<Customer> GetCustomers()
        {
            var customerDb = _testDatabaseEntities.Customers.ToList();

            var customers = customerDb.Select(customerDbValue => _mapper.Map<ORM.Data.Customer, Customer>(customerDbValue)).ToList();

            return customers;
        }

        public Customer SaveCustomer(Customer customer)
        {
            var customerDb = _mapper.Map<Customer, ORM.Data.Customer>(customer);

            _testDatabaseEntities.Customers.Add(customerDb);

            Thread.Sleep(TimeSpan.FromSeconds(10));

            _testDatabaseEntities.SaveChanges();

            return _mapper.Map<ORM.Data.Customer, Customer>(customerDb);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _testDatabaseEntities.Dispose();
                }
            }

            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
