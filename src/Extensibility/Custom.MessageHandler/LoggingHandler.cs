using System.Net.Http;
using Serilog.Utility;

namespace Custom.MessageHandler
{
    public class LoggingHandler : StartupHandler
    {
        private readonly ILoggerSetup _loggerSetup;

        public LoggingHandler(ILoggerSetup loggerSetup)
        {
            _loggerSetup = loggerSetup;
        }

        protected override void IncommingMessage(string correlationId, HttpRequestMessage httpRequestMessage)
        {
            _loggerSetup.CustomInformation(informationMessage: correlationId, request: httpRequestMessage.Content.ReadAsStringAsync().Result,
                host: "LoggerHandler-Incoming");
        }

        protected override void OutgoingMessage(string correlationId, HttpRequestMessage httpRequestMessage,
            HttpResponseMessage httpResponseMessage)
        {
            _loggerSetup.CustomInformation(informationMessage: correlationId, request: httpRequestMessage.Content.ReadAsStringAsync().Result,
                response: httpResponseMessage.Content.ReadAsStringAsync().Result, host: "LoggerHandler-Outgoing");
        }
    }
}