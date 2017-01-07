using System.Collections.Generic;
using System.Net;
using System.Web.Http.Filters;
using Common.Func.CustomActionResult;

namespace Custom.Filters.Filters
{
    public class CustomExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            var request = actionExecutedContext.Request;

            var response = new CustomHttpActionResult("InternalServerError", "Internal Server Error", request, HttpStatusCode.InternalServerError).Response;

            actionExecutedContext.Response = response;
        }
    }
}
