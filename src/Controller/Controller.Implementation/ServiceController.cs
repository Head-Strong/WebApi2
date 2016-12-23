using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;
using AutoMapper;
using Controller.Implementation.AutoMapperConfigMapper;
using Controller.Interface;
using Domains;
using Dto;
using Service.Interface;

namespace Controller.Implementation
{
    public class ServiceController : ApiController, IServiceController
    {
        private readonly IService _service;
        private readonly IMapper _mapper;

        public ServiceController(IService service, IDtoDomainMapper dtoDomainMapper)
        {
            _service = service;
            _mapper = dtoDomainMapper.ConfigureMap();
        }

        /// <summary>
        /// Provide Response
        /// </summary>
        /// <returns>Success Response</returns>
        [ResponseType(typeof(string))]
        public IHttpActionResult GetData()
        {
            return Ok(_service.GetData());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>Get List of Customers</returns>
        [ResponseType(typeof(IEnumerable<CustomerDto>))]
        public IHttpActionResult GetCustomers()
        {
            // throw new Exception();

            // return new InternalServerErrorActionResult("InvalidError", new List<string> { "Invalid Error" });

            var domainCustomers = _service.GetCustomers();

            return Ok(domainCustomers.Select(domainCustomer => _mapper.Map<Customer, CustomerDto>(domainCustomer)).ToList());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        [HttpPost]        
        [ResponseType(typeof(CustomerDto))]
        public IHttpActionResult SaveCustomer(CustomerDto customer)
        {
            // throw new Exception();

            var domainCustomer = _mapper.Map<CustomerDto, Customer>(customer);

            domainCustomer = _service.SaveCustomer(domainCustomer);

            return Ok(_mapper.Map<Customer, CustomerDto>(domainCustomer));
        }
    }
}
