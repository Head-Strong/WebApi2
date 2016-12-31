using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Web.Http.Filters;
using System.Web.Http.Results;
using Controller.Implementation.CustomActionResult;
using Dto;

namespace Custom.Filters
{
    public class CustomExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            //var response = new HttpResponseMessage().InternalServerError("InternalServerError",
              //  new List<string> {"Internal Server Error"});

            var response = new ForbiddenActionResult("Authorized", new List<string> {"Forbidden Error"}).Response;

            actionExecutedContext.Response = response;
        }
    }
}
