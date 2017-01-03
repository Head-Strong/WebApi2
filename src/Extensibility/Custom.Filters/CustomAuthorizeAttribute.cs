using System;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using Service.Interface;

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
            //try
            //{
            //    var _service =
            //        actionContext.ActionDescriptor.Configuration.DependencyResolver.GetService(typeof(IService)) as
            //            IService;
                                    
            //    var data = _service.GetCustomers().ToList().FirstOrDefault();

            //    var claimsIdentity = new ClaimsIdentity(data.Name);

            //    var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

            //    Thread.CurrentPrincipal = claimsPrincipal;

            //    if (HttpContext.Current.User == null)
            //    {
            //        HttpContext.Current.User = claimsPrincipal;
            //    }
            //}
            //catch (Exception e)
            //{
            //    Console.WriteLine(e);
            //    throw;
            //}
        }
    }
}
