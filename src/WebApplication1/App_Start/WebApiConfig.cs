using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Dispatcher;
using System.Web.Http.ExceptionHandling;
using System.Web.Http.Filters;
using Custom.Filters.Filters;
using Custom.Filters.Models;
using Custom.Filters.Providers;
using Custom.Loggers;
using Custom.MessageHandler;
using DependencyRegisterResolver;
using Serilog.Utility;
using Service.Interface;

namespace WebApplication1
{
    /// <summary>
    /// 
    /// </summary>
    public static class WebApiConfig
    {
        /// <summary>
        /// 
        /// </summary>
        public static ILoggerSetup LoggerSetup { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="config"></param>
        public static void Register(HttpConfiguration config)
        {
            config.EnableCors(new EnableCorsAttribute("*", "*", "*"));
            ConatinerSetup(config);
            LoggerSetup = config.DependencyResolver.GetService(typeof(ILoggerSetup)) as ILoggerSetup;
            MapRoutes(config);
            FilterConfigurationReader.Get();
            ConfigureHandlers(config);
            RegisterFilters(config);
            ConfigureLoggers(config);
            SwaggerConfig.Register();

            config.Services.Replace(typeof(IHttpControllerSelector), new ApiVersioningSelector((config)));
        }

        private static void ConfigureHandlers(HttpConfiguration config)
        {
            config.MessageHandlers.Add(new LoggingHandler(LoggerSetup));
            config.Services.Replace(typeof(IExceptionHandler), new CustomExceptionHandler());
        }

        private static void ConfigureLoggers(HttpConfiguration config)
        {
            config.Services.Replace(typeof(IExceptionLogger), new CustomExceptionLogger(LoggerSetup));
            //config.Services.Replace(typeof(IFormatterLogger), new CustomFormatLogger());
        }

        private static void MapRoutes(HttpConfiguration config)
        {

            var resolvedService = config.DependencyResolver.GetService(typeof(IService)) as IService;
            //config.MessageHandlers.Add(new AuthenticationHandler(resolvedService));
            // Web API routes

            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
               "Swagger",
               "api/swagger/ui/{index}"
           );

            config.Routes.MapHttpRoute(
                "DefaultApi",
                "api/{version}/{controller}/{action}/{id}",
                new { id = RouteParameter.Optional },
                constraints:null,                
                handler: new AuthenticationHandler(resolvedService) { InnerHandler = new HttpControllerDispatcher(config) }            
            );           
        }

        private static void ConatinerSetup(HttpConfiguration config)
        {
            // Web API configuration and services
            var container = new ContainerFactory(ContainerFactory.Container.Unity).Instantiate();
            container = container.RegisterContainer();
            config.DependencyResolver = container.ResolveContainer().DependencyResolver;
        }

        private static void RegisterFilters(HttpConfiguration config)
        {
            config.Services.Add(typeof(IFilterProvider), new CustomFilterProvider());
            config.Filters.Add(new CustomExceptionFilter());
            config.Filters.Add(new LoggingFilter());
            config.Services.Add(typeof(System.Web.Http.Validation.ModelValidatorProvider), new CustomModelValidatorProvider());
        }
    }
}
