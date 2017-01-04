using System.Collections.Generic;
using System.Web.Http.Filters;
using Controller.Implementation.CustomActionResult;

namespace Custom.Filters
{
    public class CustomExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            var response = new ForbiddenActionResult("Authorized", new List<string> { "Forbidden Error" }).Response;

            actionExecutedContext.Response = response;
        }
    }
}
