using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Web.Http;
using System.Web.Http.Description;
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

        /// <summary>
        /// Provide Response
        /// </summary>
        /// <returns>Success Response</returns>
        [ResponseType(typeof(string))]
        public IHttpActionResult GetData()
        {
            var request = Request.Headers;

            var guidValues = request.GetValues("Guid");
            var guid = string.Empty;

            var authKey = request.Authorization;

            if (guidValues != null)
            {
                guid = guidValues.FirstOrDefault();
            }

            Thread.Sleep(TimeSpan.FromSeconds(int.Parse(ConfigurationManager.AppSettings["Sleep"])));

            //using (var httpClient = new HttpClient())
            //{
            //    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            //    var response = httpClient.GetAsync("http://www.google.com").GetAwaiter().GetResult();
            //    var data = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            //    Console.WriteLine(data);
            //    Console.WriteLine("===============");
            //}

            return Ok(guid + "~" + authKey.Parameter);
        }

        /// <summary>
        /// Get Customers
        /// </summary>
        /// <returns>Get List of Customers</returns>
        [ResponseType(typeof(IEnumerable<CustomerDto>))]
        [ResponseType(typeof(ErrorDto))]
        public IHttpActionResult GetCustomers()
        {
            // throw new Exception();

            // return new InternalServerErrorActionResult("InvalidError", new List<string> { "Invalid Error" });

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
            // throw new Exception();
            var validator = new CustomerDtoValidator();
            var validationResult = validator.Validate(customer);

            if (!validationResult.IsValid)
            {
                return new InternalServerErrorActionResult("InternalError", validationResult.Errors.Select(x => x.ErrorMessage).ToList());
            }

            var domainCustomer = _mapper.Map<CustomerDto, Customer>(customer);

            domainCustomer = _service.SaveCustomer(domainCustomer);

            var customerDto = _mapper.Map<Customer, CustomerDto>(domainCustomer);

            //var response = Request.CreateResponse<CustomerDto>(HttpStatusCode.Created, customerDto);

            //var uri = Url.Link("DefaultApi", new { id = customerDto.Id });
            //response.Headers.Location = new Uri(uri);

            return Ok(customerDto);
        }
    }
}
