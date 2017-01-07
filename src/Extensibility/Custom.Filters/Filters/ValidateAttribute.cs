using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using Common.Func.CustomActionResult;

namespace Custom.Filters.Filters
{
    public class ValidateAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            var request = actionContext.Request;

            if (!actionContext.ModelState.IsValid)
            {
                var errors = new List<string>();
                foreach (var modelstates in actionContext.ModelState.Values)
                {
                    errors.AddRange(modelstates.Errors.Select(error => !string.IsNullOrEmpty(error.ErrorMessage)
                                            ? error.ErrorMessage
                                            : "Input is not in right format"));
                }

                actionContext.Response = new CustomHttpActionResult("BadRequest", errors.Distinct().ToList(), request, HttpStatusCode.BadRequest).Response;
            }
        }
    }
}
