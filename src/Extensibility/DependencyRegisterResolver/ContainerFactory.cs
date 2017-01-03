using System;
using System.Web.Http.Dependencies;
using Castle.Windsor;
using DependencyRegisterResolver.Register;
using DependencyRegisterResolver.Resolver;
using Microsoft.Practices.Unity;

namespace DependencyRegisterResolver
{
    public class ContainerFactory
    {
        private readonly Container _container;
        private IDependencyRegister<IUnityContainer> _unityContainer;
        private IDependencyRegister<IWindsorContainer> _windsorContainer;
        public IDependencyResolver DependencyResolver { get; private set; }

        public ContainerFactory(Container container)
        {
            _container = container;
        }

        public ContainerFactory Instantiate()
        {
            switch (_container)
            {
                case Container.Unity:
                    _unityContainer = new UnityContainerRegister();
                    break;

                case Container.CastleWindsor:
                    _windsorContainer = new CastleWinsdorRegister();
                    break;

                case Container.StructureMap:
                case Container.Ninject:
                    throw new NotImplementedException();
                default:
                    throw new ArgumentOutOfRangeException(nameof(_container), _container, null);
            }

            return this;
        }

        public ContainerFactory RegisterContainer()
        {
            switch (_container)
            {
                case Container.Unity:
                    CustomRegisterdContainer = _unityContainer.Register();
                    break;

                case Container.CastleWindsor:
                    _windsorContainer.Register();
                    break;

                case Container.StructureMap:
                case Container.Ninject:
                    throw new NotImplementedException();
                default:
                    throw new ArgumentOutOfRangeException(nameof(_container), _container, null);
            }

            return this;
        }

        public static IUnityContainer CustomRegisterdContainer { get; set; }

        public ContainerFactory ResolveContainer()
        {
            switch (_container)
            {
                case Container.Unity:
                    DependencyResolver = new UnityContainerResolver(_unityContainer.GetContainer);
                    break;

                case Container.CastleWindsor:
                    DependencyResolver = new WindsorContainerResolver(_windsorContainer.GetContainer);
                    break;

                case Container.StructureMap:
                case Container.Ninject:
                    throw new NotImplementedException();
                default:
                    throw new ArgumentOutOfRangeException(nameof(_container), _container, null);
            }

            return this;
        }

        public enum Container
        {
            Unity,
            CastleWindsor,
            StructureMap,
            Ninject
        }
    }
}