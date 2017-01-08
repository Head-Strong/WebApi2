using System;
using System.Web.Http;
using Serilog.Utility;

namespace WebApplication1
{
    /// <summary>
    /// 
    /// </summary>
    public class WebApiApplication : System.Web.HttpApplication
    {
        /// <summary>
        /// 
        /// </summary>
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
        }

        /// <summary>
        /// 
        /// </summary>
        protected void Application_Error()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        protected void Application_End()
        {
            // https://github.com/serilog/serilog/wiki/Lifecycle-of-Loggers
            // https://blog.merbla.com/2016/07/06/serilog-log-closeandflush/
            WebApiConfig.LoggerSetup?.CloseAndFlush();
        }
    }
}
