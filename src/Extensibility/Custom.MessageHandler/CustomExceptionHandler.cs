using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.ExceptionHandling;
using Common.Func.CustomActionResult;

namespace Custom.MessageHandler
{
    public class CustomExceptionHandler : ExceptionHandler
    {
        public override Task HandleAsync(ExceptionHandlerContext context, CancellationToken cancellationToken)
        {
            const string errorMessage = "An unexpected error occured";

            var response = new CustomHttpActionResult("InternalServerError",
                                    errorMessage, context.Request, HttpStatusCode.InternalServerError);
            context.Result = response;

            return base.HandleAsync(context, cancellationToken);
        }
    }
}