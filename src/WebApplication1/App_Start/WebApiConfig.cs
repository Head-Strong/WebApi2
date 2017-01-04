using System.Data.Entity;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.ExceptionHandling;
using System.Web.Http.Filters;
using System.Web.Http.Validation;
using System.Web.ModelBinding;
using Custom.Filters;
using Custom.Filters.Models;
using Custom.MessageHandler;
using DependencyRegisterResolver;
using DependencyRegisterResolver.Register;
using DependencyRegisterResolver.Resolver;
using FluentValidation.WebApi;
using ORM.Data;
using Service.Interface;
using ModelValidatorProvider = System.Web.ModelBinding.ModelValidatorProvider;

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
            config.EnableCors(new EnableCorsAttribute("*","*","*"));

            // Web API configuration and services
            var container = new ContainerFactory(ContainerFactory.Container.Unity).Instantiate();

            container = container.RegisterContainer();

            config.DependencyResolver = container.ResolveContainer().DependencyResolver;
            
            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            var data12 = FilterConfigurationReader.Get();

            var resolvedService = config.DependencyResolver.GetService(typeof(IService)) as IService;

            config.MessageHandlers.Add(new AuthenticationHandler(resolvedService));

            config.Services.Add(typeof(IFilterProvider), new CustomFilterProvider());
            
            config.Filters.Add(new CustomExceptionFilter());

            config.Filters.Add(new LoggingFilter());

            config.Services.Replace(typeof(IExceptionLogger), new CustomExceptionLogger());

            config.Services.Replace(typeof(IExceptionHandler), new CustomExceptionHandler());

            FluentValidationModelValidatorProvider.Configure(config);

            config.Services.Add(typeof(System.Web.Http.Validation.ModelValidatorProvider), new CustomModelValidatorProvider());

            SwaggerConfig.Register();
        }
    }
}
