using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading;

namespace Controller.Implementation.CustomActionResult
{
    public class InternalServerErrorActionResult : BaseHttpActionResult
    {
        public InternalServerErrorActionResult(string errorCategory, List<string> errorDescriptions)
            : base(errorCategory, errorDescriptions)
        {
            StatusCode = HttpStatusCode.InternalServerError;
        }

    }

    public class UnauthorizedActionResult : BaseHttpActionResult
    {
        public UnauthorizedActionResult(string errorCategory, List<string> errorDescriptions)
            : base(errorCategory, errorDescriptions)
        {
            StatusCode = HttpStatusCode.Unauthorized;
        }
    }

    public class ForbiddenActionResult : BaseHttpActionResult
    {
        public ForbiddenActionResult(string errorCategory, List<string> errorDescriptions)
            : base(errorCategory, errorDescriptions)
        {
            StatusCode = HttpStatusCode.Forbidden;
        }
    }
}