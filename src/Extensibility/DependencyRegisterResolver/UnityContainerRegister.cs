using Controller.Implementation;
using Controller.Implementation.AutoMapperConfigMapper;
using Controller.Interface;
using Microsoft.Practices.Unity;
using ORM.Data;
using Repository.Implementation.AutoMapperConfigMapper;
using Repository.Interface;
using Service.Interface;

namespace DependencyRegisterResolver
{
    public class UnityContainerRegister : IDependencyRegister<IUnityContainer>
    {
        private static IUnityContainer _container;

        public IUnityContainer GetContainer => _container;

        public UnityContainerRegister() : this(null)
        {
        }

        private UnityContainerRegister(IUnityContainer unityContainer)
        {
            _container = unityContainer ?? new UnityContainer();
        }

        public IUnityContainer Register()
        {
            RegisterRepository();
            RegisterServices();
            RegisterController();

            return _container;
        }

        private void RegisterRepository()
        {
            _container.RegisterInstance(typeof(TestDatabaseEntities));

            _container.RegisterType<IAutoMapperConfigMapper, AutoMapperConfigMapper>(
                new ContainerControlledLifetimeManager());

            _container.RegisterType<ITestRepository, Repository.Implementation.TestRepository>(
                new HierarchicalLifetimeManager());
        }

        private void RegisterServices()
        {
            _container.RegisterType<IService, Service.Implementation.Service>(new HierarchicalLifetimeManager());
        }

        private void RegisterController()
        {
            _container.RegisterType<IDtoDomainMapper, DtoDomainMapper>(new ContainerControlledLifetimeManager());
            _container.RegisterType<IServiceController, ServiceController>(new HierarchicalLifetimeManager());
        }
    }
}
