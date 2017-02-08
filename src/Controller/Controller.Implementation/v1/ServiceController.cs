using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using AutoMapper;
using Controller.Implementation.AutoMapperConfigMapper;
using Controller.Interface;
using Domains;
using Dto;
using Service.Interface;
using Swashbuckle.Swagger.Annotations;

namespace Controller.Implementation.v1
{
    /// <summary>
    /// 
    /// </summary>
    public class ServiceController : ApiController, IServiceController
    {
        private readonly IService _service;
        private readonly IMapper _mapper;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="service"></param>
        /// <param name="dtoDomainMapper"></param>
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
        public virtual IHttpActionResult GetData()
        {
            return Ok("Ok V1");
        }

        /// <summary>
        /// Get Customers
        /// </summary>
        /// <returns>Get List of Customers</returns>
        [SwaggerResponse(HttpStatusCode.OK, "Get List of Customers", typeof(IEnumerable<CustomerDto>))]
        [SwaggerResponse(HttpStatusCode.BadRequest, "Validation Errors", typeof(ErrorDto))]
        [SwaggerResponse(HttpStatusCode.Unauthorized, "Authorization Token Is Missing", typeof(ErrorDto))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "Internal Server Error", typeof(ErrorDto))]
        public IHttpActionResult GetCustomers()
        {
            var domainCustomers = _service.GetCustomers();

            return Ok(domainCustomers.Select(domainCustomer => _mapper.Map<Customer, CustomerDto>(domainCustomer)).ToList());
        }

        /// <summary>
        /// Save Customer
        /// </summary>
        /// <param name="customer">Customer</param>
        /// <returns>Saved Customer</returns>
        [HttpPost]
        [ResponseType(typeof(CustomerDto))]
        public IHttpActionResult SaveCustomer(CustomerDto customer)
        {
            var domainCustomer = _mapper.Map<CustomerDto, Customer>(customer);

            domainCustomer = _service.SaveCustomer(domainCustomer);

            var customerDto = _mapper.Map<Customer, CustomerDto>(domainCustomer);

            return Ok(customerDto);
        }
    }
}
