using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.ExceptionHandling;
using System.Web.Http.Results;

namespace Custom.MessageHandler
{
    public class CustomExceptionHandler : ExceptionHandler
    {
        public override Task HandleAsync(ExceptionHandlerContext context, CancellationToken cancellationToken)
        {
            const string errorMessage = "An unexpected error occured";
            var response = context.Request.CreateResponse(HttpStatusCode.InternalServerError,
                new
                {
                    Message = errorMessage
                });

            response.Headers.Add("X-Error", errorMessage);

            context.Result = new ResponseMessageResult(response);

            return base.HandleAsync(context, cancellationToken);
        }        
    }
}