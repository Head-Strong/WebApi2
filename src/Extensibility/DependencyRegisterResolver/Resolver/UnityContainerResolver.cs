using System;
using System.Collections.Generic;
using System.Web.Http.Dependencies;
using Microsoft.Practices.Unity;

namespace DependencyRegisterResolver.Resolver
{
    internal class UnityContainerResolver : IDependencyResolver
    {
        private readonly IUnityContainer _container;

        public UnityContainerResolver(IUnityContainer container)
        {
            if (container == null)
            {
                throw new ArgumentNullException(nameof(container));
            }

            _container = container;
        }

        public object GetService(Type serviceType)
        {
            try
            {
                return _container.Resolve(serviceType);
            }
            catch (ResolutionFailedException)
            {
                return null;
            }
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            try
            {
                return _container.ResolveAll(serviceType);
            }
            catch (ResolutionFailedException)
            {
                return new List<object>();
            }
        }

        public void Dispose()
        {
            _container.Dispose();
        }

        IDependencyScope IDependencyResolver.BeginScope()
        {
            var child = _container.CreateChildContainer();
            return new UnityContainerResolver(child);
        }
    }
}