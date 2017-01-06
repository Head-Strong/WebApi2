using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using Custom.Filters.Models;

namespace Custom.Filters.Providers
{
    public class CustomFilterProvider : IFilterProvider
    {
        public IEnumerable<FilterInfo> GetFilters(HttpConfiguration configuration, HttpActionDescriptor actionDescriptor)
        {
            var actionName = actionDescriptor.ActionName;
            var controllerName = actionDescriptor.ControllerDescriptor.ControllerName;

            var filterConfig = FilterConfigurationReader.Get()
                .FirstOrDefault(x => x.ActionName.Equals(actionName, StringComparison.OrdinalIgnoreCase)
                                     && x.ControllerName.Equals(controllerName, StringComparison.OrdinalIgnoreCase));

            if (filterConfig?.Filter == null) yield break;

            const string assemblyName = "Custom.Filters";

            var filterName = assemblyName + ".Filters." + filterConfig.Filter;

            var typeofFilter = Type.GetType(filterName + "," + assemblyName);

            if (typeofFilter == null)
            {
                throw new InvalidCastException(nameof(typeofFilter));
            }

            if (filterConfig.Roles != null)
            {
                yield return new FilterInfo((IFilter)Activator.CreateInstance(typeofFilter, new object[] { filterConfig.Roles }), FilterScope.Action);
            }
            else
            {
                yield return new FilterInfo((IFilter)Activator.CreateInstance(typeofFilter), FilterScope.Action);
            }
        }
    }
}