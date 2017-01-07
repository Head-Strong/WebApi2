using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using Dto;

namespace Common.Func.CustomActionResult
{
    /// <summary>
    /// 
    /// </summary>
    public class CustomHttpActionResult : IHttpActionResult
    {
        private readonly string _errorCategory;
        private readonly List<string> _errorDescriptions;
        private readonly HttpRequestMessage _requestMessage;
        private readonly HttpStatusCode _statusCode;
        private HttpResponseMessage _httpResponseMessage;

        public CustomHttpActionResult(string errorCategory, List<string> errorDescriptions, HttpRequestMessage requestMessage, HttpStatusCode statusCode)
        {
            _errorCategory = errorCategory;
            _errorDescriptions = errorDescriptions;
            _requestMessage = requestMessage;
            _statusCode = statusCode;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="errorCategory"></param>
        /// <param name="errorDescriptions"></param>
        /// <param name="requestMessage"></param>
        /// <param name="statusCode"></param>
        public CustomHttpActionResult(string errorCategory, string errorDescriptions,
                HttpRequestMessage requestMessage, HttpStatusCode statusCode) : this(errorCategory, new List<string> { errorDescriptions }, requestMessage, statusCode)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            _httpResponseMessage = new HttpResponseMessage
            {
                StatusCode = _statusCode,
                Content = PrepareErrorContent(_errorCategory, _errorDescriptions),
                RequestMessage = _requestMessage
            };

            return Task.FromResult(_httpResponseMessage);
        }

        /// <summary>
        /// 
        /// </summary>
        public HttpResponseMessage Response => ExecuteAsync(new CancellationToken()).Result;

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