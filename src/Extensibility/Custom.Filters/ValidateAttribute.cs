using System.Collections.Generic;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using Controller.Implementation.CustomActionResult;

namespace Custom.Filters
{
    public class ValidateAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            if (!actionContext.ModelState.IsValid)
            {
                var errors = new List<string>();
                foreach (var error in actionContext.ModelState)
                {
                    errors.Add(error.Value.ToString());
                }

                actionContext.Response = new BadActionResult("Bad Request", errors).Response;
            }
        }
    }
}
