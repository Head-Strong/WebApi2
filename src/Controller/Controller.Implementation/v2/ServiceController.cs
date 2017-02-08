using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;
using AutoMapper;
using Controller.Implementation.AutoMapperConfigMapper;
using Controller.Interface;
using Domains;
using Dto;
using Service.Interface;

namespace Controller.Implementation.v2
{
    /// <summary>
    /// 
    /// </summary>
    public class ServiceController : v1.ServiceController, IServiceControllerV2
    {
        private readonly IService _service;
        private readonly IMapper _mapper;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="service"></param>
        /// <param name="dtoDomainMapper"></param>
        public ServiceController(IService service, IDtoDomainMapper dtoDomainMapper) : base(service, dtoDomainMapper)
        {
            _service = service;
            _mapper = dtoDomainMapper.ConfigureMap();
        }

        /// <summary>
        /// Provide Response
        /// </summary>
        /// <returns>Success Response</returns>
        [ResponseType(typeof(string))]
        public override IHttpActionResult GetData()
        {
            return Ok("Ok V2");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IHttpActionResult GetCustomerById(int id)
        {
            var domainCustomer = _service.GetCustomers().FirstOrDefault(x => x.Id == id);

            if (domainCustomer == null)
            {
                NotFound();
            }

            var dtoCustomer = _mapper.Map<Customer, CustomerDto>(domainCustomer);

            return Ok(dtoCustomer);
        }
    }
}
