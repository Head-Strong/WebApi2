using System;
using System.Net.Http.Formatting;

namespace Custom.Loggers
{
    public class CustomFormatLogger : IFormatterLogger
    {
        public void LogError(string errorPath, string errorMessage)
        {
            throw new NotImplementedException();
        }

        public void LogError(string errorPath, Exception exception)
        {
            throw new NotImplementedException();
        }
    }
}
