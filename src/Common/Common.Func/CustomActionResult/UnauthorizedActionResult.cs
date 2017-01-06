using System.Collections.Generic;
using System.Net;
using System.Net.Http;

namespace Common.Func.CustomActionResult
{
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
        /// <param name="requestMessage"></param>
        public UnauthorizedActionResult(string errorCategory, List<string> errorDescriptions, HttpRequestMessage requestMessage)
            : base(errorCategory, errorDescriptions, requestMessage)
        {
            StatusCode = HttpStatusCode.Unauthorized;
        }
    }
}