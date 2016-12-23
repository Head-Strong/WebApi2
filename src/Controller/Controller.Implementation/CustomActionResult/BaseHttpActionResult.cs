using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using Dto;

namespace Controller.Implementation.CustomActionResult
{
    public abstract class BaseHttpActionResult : IHttpActionResult
    {
        protected HttpStatusCode StatusCode;
        private readonly string _errorCategory;
        private readonly List<string> _errorDescriptions;
        private HttpResponseMessage _httpResponseMessage;

        protected BaseHttpActionResult(string errorCategory, List<string> errorDescriptions)
        {
            _errorCategory = errorCategory;
            _errorDescriptions = errorDescriptions;
        }

        public Task<HttpResponseMessage> ExecuteAsync(System.Threading.CancellationToken cancellationToken)
        {
            _httpResponseMessage = new HttpResponseMessage
            {
                StatusCode = StatusCode,
                Content = PrepareErrorContent(_errorCategory, _errorDescriptions)
            };

            return Task.FromResult(_httpResponseMessage);
        }

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