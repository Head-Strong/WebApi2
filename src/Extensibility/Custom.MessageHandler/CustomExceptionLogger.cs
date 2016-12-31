using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.ExceptionHandling;

namespace Custom.MessageHandler
{
    public class CustomExceptionLogger : ExceptionLogger
    {
        public override void Log(ExceptionLoggerContext context)
        {
            if (ShouldLog(context))
            {
                EventLog.WriteEntry("WebApi", context.Exception.Message);
            }
        }


    }
}