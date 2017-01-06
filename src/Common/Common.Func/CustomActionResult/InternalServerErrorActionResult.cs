using System.Collections.Generic;
using System.Net;
using System.Net.Http;

namespace Common.Func.CustomActionResult
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
        /// <param name="requestMessage"></param>
        public InternalServerErrorActionResult(string errorCategory, List<string> errorDescriptions, HttpRequestMessage requestMessage)
            : base(errorCategory, errorDescriptions, requestMessage)
        {
            StatusCode = HttpStatusCode.InternalServerError;
        }

    }
}