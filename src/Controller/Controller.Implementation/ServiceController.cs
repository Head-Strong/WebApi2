using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using Controller.Implementation.AutoMapperConfigMapper;
using Controller.Implementation.CustomActionResult;
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

        public IHttpActionResult GetData()
        {
            return Ok(_service.GetData());
        }

        public IHttpActionResult GetCustomers()
        {
            throw new Exception();

            return new InternalServerErrorActionResult("InvalidError", new List<string> { "Invalid Error" });

            var domainCustomers = _service.GetCustomers();

            return Ok(domainCustomers.Select(domainCustomer => _mapper.Map<Customer, CustomerDto>(domainCustomer)).ToList());
        }

        [HttpPost]
        public IHttpActionResult SaveCustomer(CustomerDto customer)
        {
            // throw new Exception();

            var domainCustomer = _mapper.Map<CustomerDto, Customer>(customer);

            domainCustomer = _service.SaveCustomer(domainCustomer);

            return Ok(_mapper.Map<Customer, CustomerDto>(domainCustomer));
        }
    }
}
