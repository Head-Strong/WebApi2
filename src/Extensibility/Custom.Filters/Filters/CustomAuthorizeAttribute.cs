using System.Web.Http;
using System.Web.Http.Controllers;

namespace Custom.Filters.Filters
{
    public class CustomAuthorizeAttribute : AuthorizeAttribute
    {
        private readonly string[] _roles;

        public CustomAuthorizeAttribute(string[] roles)
        {
            _roles = roles;
        }

        public override void OnAuthorization(HttpActionContext actionContext)
        {
           
        }
    }
}
