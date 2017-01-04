﻿using System;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
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
            //var authorization = request.Headers.Authorization;

            //if (string.IsNullOrWhiteSpace(authorization.Scheme))
            //{
            //    var responseMessage = PrepareResponseMessage("Schema is not present.");
            //    return responseMessage;
            //}

            //if (string.IsNullOrWhiteSpace(authorization.Parameter))
            //{
            //    var responseMessage = PrepareResponseMessage("Authorization key is missing.");
            //    return responseMessage;
            //}

           return await base.SendAsync(request, cancellationToken);
        }

        private static HttpResponseMessage PrepareResponseMessage(string message)
        {
            var responseMessage = new HttpResponseMessage(HttpStatusCode.BadRequest)
            {
                Content = new StringContent(message, Encoding.UTF8)
            };

            return responseMessage;
        }
    }
}
