using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;
using System.Web.Http.Routing;
using static System.String;

namespace WebApplication1
{
    /// <summary>
    /// 
    /// </summary>
    public class ApiVersioningSelector : DefaultHttpControllerSelector
    {
        private readonly HttpConfiguration _configuration;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="configuration"></param>
        public ApiVersioningSelector(HttpConfiguration configuration) : base(configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public override HttpControllerDescriptor SelectController(HttpRequestMessage request)
        {
            var assembliesResolver = _configuration.Services.GetAssembliesResolver();
            var controllerResolver = _configuration.Services.GetHttpControllerTypeResolver();
            var controllerTypes = controllerResolver.GetControllerTypes(assembliesResolver);
            
            var routeData = request.GetRouteData();

            var version = routeData.Values["version"].ToString();

            if (IsNullOrWhiteSpace(version))
            {
                version = "v1";
            }

            version = "." + version + ".";

            var versionController = controllerTypes.FirstOrDefault(x => x.FullName.Contains(version));

            var specificControllerDescriptor = new HttpControllerDescriptor
            {
                Configuration = _configuration,
                ControllerType = versionController
            };

            return specificControllerDescriptor;
        }
    }
}