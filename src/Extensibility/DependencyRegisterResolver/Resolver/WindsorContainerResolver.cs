using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Dependencies;
using Castle.MicroKernel;
using Castle.MicroKernel.Lifestyle;
using Castle.Windsor;
using Microsoft.Practices.Unity;
using IDependencyResolver = System.Web.Http.Dependencies.IDependencyResolver;

namespace DependencyRegisterResolver.Resolver
{
    internal class WindsorContainerResolver : IDependencyResolver
    {
        private readonly IWindsorContainer _container;

        public WindsorContainerResolver(IWindsorContainer container)
        {
            if (container == null)
            {
                throw new ArgumentNullException(nameof(container));
            }

            _container = container;
        }

        public object GetService(Type serviceType)
        {
            return _container.Kernel.HasComponent(serviceType) ? _container.Kernel.Resolve(serviceType) : null;
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            try
            {
                return _container.Kernel.ResolveAll(serviceType).Cast<object>();
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
            return new WindsorDependencyScope(_container.Kernel);            
        }
    }

    internal class WindsorDependencyScope : IDependencyScope
    {
        private readonly IKernel _container;
        private readonly IDisposable _scope;

        public WindsorDependencyScope(IKernel container)
        {
            _container = container;
            _scope = container.BeginScope();
        }

        public object GetService(Type serviceType)
        {
            return _container.HasComponent(serviceType) ? _container.Resolve(serviceType) : null;
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return _container.ResolveAll(serviceType).Cast<object>();
        }

        public void Dispose()
        {
            _scope.Dispose();
        }
    }
}
