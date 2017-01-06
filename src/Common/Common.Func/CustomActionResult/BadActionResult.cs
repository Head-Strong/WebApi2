using System.Collections.Generic;
using System.Net;
using System.Net.Http;

namespace Common.Func.CustomActionResult
{
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
        /// <param name="requestMessage"></param>
        public BadActionResult(string errorCategory, List<string> errorDescriptions, HttpRequestMessage requestMessage)
            : base(errorCategory, errorDescriptions, requestMessage)
        {
            StatusCode = HttpStatusCode.BadRequest;
        }
    }
}