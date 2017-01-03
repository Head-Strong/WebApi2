using System;
using System.Data.Entity;
using Controller.Implementation;
using Controller.Implementation.AutoMapperConfigMapper;
using Controller.Interface;
using Microsoft.Practices.Unity;
using ORM.Data;
using Repository.Implementation;
using Repository.Implementation.AutoMapperConfigMapper;
using Repository.Interface;
using Service.Interface;

namespace DependencyRegisterResolver.Register
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

        #region Helper Methods
        private static void RegisterRepository()
        {
            _container.RegisterInstance(typeof(TestDatabaseEntities), new PerThreadLifetimeManager());

            _container.RegisterType<IAutoMapperConfigMapper, AutoMapperConfigMapper>(new ContainerControlledLifetimeManager());

            _container.RegisterType<ITestRepository, TestRepository>(new HierarchicalLifetimeManager());

            _container.RegisterType<IRepository<ORM.Data.Customer>, Repository<ORM.Data.Customer>>(new HierarchicalLifetimeManager());

            _container.RegisterType<IRepository<ORM.Data.Address>, Repository<ORM.Data.Address>>(new HierarchicalLifetimeManager());

            _container.RegisterType<ICustomerRepository, CustomerRepository>(new HierarchicalLifetimeManager());

            _container.RegisterType<IAddressRepository, AddressRepository>(new HierarchicalLifetimeManager());
            
            _container.RegisterType<IUnitofWork, UnitofWork>(new HierarchicalLifetimeManager());
        }

        private static void RegisterServices()
        {
            _container.RegisterType<IService, Service.Implementation.Service>
                                (new InjectionConstructor(_container.Resolve<IUnitofWork>()));
        }

        private static void RegisterController()
        {
            _container.RegisterType<IDtoDomainMapper, DtoDomainMapper>(new ContainerControlledLifetimeManager());
            _container.RegisterType<IServiceController, ServiceController>(new HierarchicalLifetimeManager());
        }


        public static Tuple<DbContext, IService, IUnitofWork> GetService()
        {
            var context = _container.Resolve<TestDatabaseEntities>();

            var service = _container.Resolve<IService>();

            var uow = _container.Resolve<IUnitofWork>();

            return new Tuple<DbContext, IService, IUnitofWork>(context, service, uow);
        }

        #endregion
    }
}
