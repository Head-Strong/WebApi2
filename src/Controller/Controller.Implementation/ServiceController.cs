﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
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
            //var claimsPrincipal = Thread.CurrentPrincipal as ClaimsPrincipal;
            //var commaSepratedProfiles = string.Empty;
            //if (claimsPrincipal != null)
            //{
            //    var claims = claimsPrincipal
            //        .Claims.ToList().FindAll(x => x.Type.Contains("role"));

            //    var claimsAssociatedWithUser = claims.Select(claim => claim.Value).ToArray();

            //    commaSepratedProfiles = string.Join(",", claimsAssociatedWithUser);
            //}

            //var request = Request.Headers;
            //var guidValues = request.GetValues("Guid");
            //var guid = string.Empty;

            //var authKey = request.Authorization;

            //if (guidValues != null)
            //{
            //    guid = guidValues.FirstOrDefault();
            //}

            ////Thread.Sleep(TimeSpan.FromSeconds(int.Parse(ConfigurationManager.AppSettings["Sleep"])));

            //return Ok(guid + "~" + authKey.Parameter + "~" + commaSepratedProfiles);

            return Ok("Ok");
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
            var domainCustomer = _mapper.Map<CustomerDto, Customer>(customer);

            domainCustomer = _service.SaveCustomer(domainCustomer);

            var customerDto = _mapper.Map<Customer, CustomerDto>(domainCustomer);

            return Ok(customerDto);
        }
    }
}
