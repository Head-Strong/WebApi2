using System.Collections.Generic;
using System.Net;
using System.Net.Http;

namespace Dto
{
    /// <summary>
    /// 
    /// </summary>
    public static class ErrorBuilder
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="httpResponseMessage"></param>
        /// <param name="errorCategory"></param>
        /// <param name="errorDescriptions"></param>
        /// <returns></returns>
        public static HttpResponseMessage InternalServerError(this HttpResponseMessage httpResponseMessage,
            string errorCategory, List<string> errorDescriptions)
        {
            httpResponseMessage.StatusCode = HttpStatusCode.InternalServerError;
            httpResponseMessage.Content = PrepareErrorContent(errorCategory, errorDescriptions);

            return httpResponseMessage;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="httpResponseMessage"></param>
        /// <param name="errorCategory"></param>
        /// <param name="errorDescriptions"></param>
        /// <returns></returns>
        public static HttpResponseMessage AuthorizationError(this HttpResponseMessage httpResponseMessage,
            string errorCategory, List<string> errorDescriptions)
        {
            httpResponseMessage.StatusCode = HttpStatusCode.Unauthorized;
            httpResponseMessage.Content = PrepareErrorContent(errorCategory, errorDescriptions);

            return httpResponseMessage;
        }

        private static StringContent PrepareErrorContent(string errorCategory, List<string> errorDescriptions)
        {
            return new StringContent(new ErrorDto
            {
                ErrorCategory = errorCategory,
                ErrorDescriptions = errorDescriptions
            }.ToString());
        }
    }
}