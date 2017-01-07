using System.Web.Http.ExceptionHandling;
using Serilog.Utility;

namespace Custom.Loggers
{
    public class CustomExceptionLogger : ExceptionLogger
    {
        private readonly ILoggerSetup _loggerSetup;

        public CustomExceptionLogger(ILoggerSetup loggerSetup)
        {
            _loggerSetup = loggerSetup;
        }

        public override void Log(ExceptionLoggerContext context)
        {
            _loggerSetup.CustomError(errorMessage: context.Exception.StackTrace, host: "CustomExceptionLogger-Log");
        }
    }
}