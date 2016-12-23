using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Results;

namespace Custom.Filters
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
