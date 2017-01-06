using System.Collections.Generic;
using Domains;
using Repository.Interface;
using Service.Interface;

namespace Service.Implementation
{
    public class Service : IService
    {
        private readonly IUnitofWork _unitofWork;

        public Service(IUnitofWork unitofWork)
        {
            _unitofWork = unitofWork;
        }

        public string GetData()
        {
            return _unitofWork.CustomerRepository.GetData();
        }

        public IEnumerable<Customer> GetCustomers()
        {
            return _unitofWork.CustomerRepository.GetCustomers();
        }

        public Customer SaveCustomer(Customer customer)
        {
            _unitofWork.CustomerRepository.SaveCustomer(customer);
            _unitofWork.Save();

            return customer;
        }

        public List<string> GetProfiles()
        {
            return _unitofWork.CustomerRepository.GetProfiles();
        }
    }
}
