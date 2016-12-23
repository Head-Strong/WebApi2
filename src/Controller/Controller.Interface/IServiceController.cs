using System.Web.Http;
using Dto;

namespace Controller.Interface
{
    public interface IServiceController
    {
        IHttpActionResult GetData();

        IHttpActionResult GetCustomers();

        IHttpActionResult SaveCustomer(CustomerDto customer);
    }
}
