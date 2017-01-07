using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Custom.MessageHandler
{
    public abstract class StartupHandler : DelegatingHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var uniqueid = $"{DateTime.Now.Ticks}{Thread.CurrentThread.ManagedThreadId}";

            await IncommingMessage(uniqueid, request);

            var response = base.SendAsync(request, cancellationToken).Result;

            await OutgoingMessage(uniqueid, request, response);

            return response;
        }

        protected abstract Task IncommingMessage(string uniqueid, HttpRequestMessage httpRequestMessage);

        protected abstract Task OutgoingMessage(string uniqueId, HttpRequestMessage httpRequestMessage, HttpResponseMessage httpResponseMessage);
    }
}
