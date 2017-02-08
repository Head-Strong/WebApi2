using System.Web.Http;

namespace Controller.Interface
{
    public interface IServiceControllerV2 : IServiceController
    {
        IHttpActionResult GetCustomerById(int id);
    }
}