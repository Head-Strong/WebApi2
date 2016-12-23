using System.Collections.Generic;
using Domains;
using Repository.Interface;
using Service.Interface;

namespace Service.Implementation
{
    public class Service : IService
    {
        private readonly ITestRepository _testRepository;

        public Service(ITestRepository testRepository)
        {
            _testRepository = testRepository;
        }

        public string GetData()
        {
            return _testRepository.GetData();
        }

        public IEnumerable<Customer> GetCustomers()
        {
            return _testRepository.GetCustomers();
        }

        public Customer SaveCustomer(Customer customer)
        {
            return _testRepository.SaveCustomer(customer);
        }
    }
}
