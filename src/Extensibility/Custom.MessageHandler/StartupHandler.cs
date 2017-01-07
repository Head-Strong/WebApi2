using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Custom.MessageHandler
{
    public abstract class StartupHandler : DelegatingHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var correlationId = $"{DateTime.Now.Ticks}{Thread.CurrentThread.ManagedThreadId}";

            IncommingMessage(correlationId, request);

            var response = base.SendAsync(request, cancellationToken).Result;

            OutgoingMessage(correlationId, request, response);

            return Task.FromResult(response);
        }

        protected abstract void IncommingMessage(string correlationId, HttpRequestMessage httpRequestMessage);

        protected abstract void OutgoingMessage(string correlationId, HttpRequestMessage httpRequestMessage, HttpResponseMessage httpResponseMessage);
    }
}
