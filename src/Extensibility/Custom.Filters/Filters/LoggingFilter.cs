using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace Custom.Filters.Filters
{
    public class LoggingFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            
        }

        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            
        }
    }
}