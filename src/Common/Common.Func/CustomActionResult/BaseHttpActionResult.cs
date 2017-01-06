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
    public abstract class BaseHttpActionResult : IHttpActionResult
    {
        private readonly string _errorCategory;
        private readonly List<string> _errorDescriptions;
        private readonly HttpRequestMessage _requestMessage;
        private HttpResponseMessage _httpResponseMessage;

        /// <summary>
        /// 
        /// </summary>
        protected HttpStatusCode StatusCode;


        /// <summary>
        /// 
        /// </summary>
        /// <param name="errorCategory"></param>
        /// <param name="errorDescriptions"></param>
        /// <param name="requestMessage"></param>
        protected BaseHttpActionResult(string errorCategory, List<string> errorDescriptions, HttpRequestMessage requestMessage)
        {
            _errorCategory = errorCategory;
            _errorDescriptions = errorDescriptions;
            _requestMessage = requestMessage;
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
                StatusCode = StatusCode,
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