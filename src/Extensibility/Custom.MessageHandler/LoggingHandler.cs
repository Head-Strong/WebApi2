using System.Net.Http;
using System.Threading.Tasks;
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

        protected override async Task IncommingMessage(string uniqueid, HttpRequestMessage httpRequestMessage)
        {
            await Task.Run(() => _loggerSetup.CustomInformation(informationMessage: uniqueid,
                                                request: httpRequestMessage.Content.ReadAsStringAsync().Result,
                                                host: "LoggerHandler-Incoming"));
        }

        protected override async Task OutgoingMessage(string uniqueId, HttpRequestMessage httpRequestMessage,
            HttpResponseMessage httpResponseMessage)
        {
            await Task.Run(() => _loggerSetup.CustomInformation(informationMessage: uniqueId,
                                                request: httpRequestMessage.Content.ReadAsStringAsync().Result,
                                                response: httpResponseMessage.Content.ReadAsStringAsync().Result,
                                                host: "LoggerHandler-Outgoing"));
        }
    }
}