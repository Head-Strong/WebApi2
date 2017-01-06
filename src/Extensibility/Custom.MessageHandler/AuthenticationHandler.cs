using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Common.Func.CustomActionResult;
using Service.Interface;

namespace Custom.MessageHandler
{
    public class AuthenticationHandler : DelegatingHandler
    {
        private readonly IService _service;

        public AuthenticationHandler(IService service)
        {
            _service = service;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var authorization = request.Headers.Authorization;

            if (authorization == null)
            {
                var responseMessage = PrepareResponseMessage("Authorization is missing.", request);
                return responseMessage;
            }

            if (string.IsNullOrWhiteSpace(authorization.Scheme))
            {
                var responseMessage = PrepareResponseMessage("Schema is not present.", request);
                return responseMessage;
            }

            if (!authorization.Scheme.Equals("Bearer", StringComparison.OrdinalIgnoreCase))
            {
                var responseMessage = PrepareResponseMessage("Authorization Schema is Bearer.", request);
                return responseMessage;
            }

            if (string.IsNullOrWhiteSpace(authorization.Parameter))
            {
                var responseMessage = PrepareResponseMessage("Authorization key is missing.", request);
                return responseMessage;
            }

            return await base.SendAsync(request, cancellationToken);
        }

        private static HttpResponseMessage PrepareResponseMessage(string message, HttpRequestMessage request)
        {
            var responseMessage = new UnauthorizedActionResult("Authorized", new List<string> { message }, request).Response;

            return responseMessage;
        }
    }
}
