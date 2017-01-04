using System.Collections.Generic;
using System.Net;

namespace Controller.Implementation.CustomActionResult
{
    /// <summary>
    /// 
    /// </summary>
    public class InternalServerErrorActionResult : BaseHttpActionResult
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="errorCategory"></param>
        /// <param name="errorDescriptions"></param>
        public InternalServerErrorActionResult(string errorCategory, List<string> errorDescriptions)
            : base(errorCategory, errorDescriptions)
        {
            StatusCode = HttpStatusCode.InternalServerError;
        }

    }

    /// <summary>
    /// 
    /// </summary>
    public class UnauthorizedActionResult : BaseHttpActionResult
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="errorCategory"></param>
        /// <param name="errorDescriptions"></param>
        public UnauthorizedActionResult(string errorCategory, List<string> errorDescriptions)
            : base(errorCategory, errorDescriptions)
        {
            StatusCode = HttpStatusCode.Unauthorized;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class ForbiddenActionResult : BaseHttpActionResult
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="errorCategory"></param>
        /// <param name="errorDescriptions"></param>
        public ForbiddenActionResult(string errorCategory, List<string> errorDescriptions)
            : base(errorCategory, errorDescriptions)
        {
            StatusCode = HttpStatusCode.Forbidden;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class BadActionResult : BaseHttpActionResult
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="errorCategory"></param>
        /// <param name="errorDescriptions"></param>
        public BadActionResult(string errorCategory, List<string> errorDescriptions)
            : base(errorCategory, errorDescriptions)
        {
            StatusCode = HttpStatusCode.BadRequest;
        }
    }
}