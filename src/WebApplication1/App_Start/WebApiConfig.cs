using System.Web.Http;
using System.Web.Http.ExceptionHandling;
using System.Web.Http.Filters;
using Custom.Filters;
using Custom.Filters.Models;
using Custom.MessageHandler;
using DependencyRegisterResolver;

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
        /// <param name="config"></param>
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            var container = new UnityContainerRegister().Register();

            config.DependencyResolver = new CustomContainerResolver(container);
            
            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            var data12 = FilterConfigurationReader.Get();

            config.Services.Add(typeof(IFilterProvider), new CustomFilterProvider());
            
            config.Filters.Add(new CustomExceptionFilter());

            config.Services.Replace(typeof(IExceptionLogger), new CustomExceptionLogger());

            config.Services.Replace(typeof(IExceptionHandler), new CustomExceptionHandler());

            SwaggerConfig.Register();
        }
    }
}
