using System.Collections.Generic;
using System.Web.Http.Filters;
using Common.Func.CustomActionResult;

namespace Custom.Filters.Filters
{
    public class CustomExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            var request = actionExecutedContext.Request;

            var response = new ForbiddenActionResult("Authorized", new List<string> { "Forbidden Error" }, request).Response;

            actionExecutedContext.Response = response;
        }
    }
}
