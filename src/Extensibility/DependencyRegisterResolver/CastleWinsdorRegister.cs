using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Controller.Implementation;
using Controller.Implementation.AutoMapperConfigMapper;
using Controller.Interface;
using ORM.Data;
using Repository.Implementation.AutoMapperConfigMapper;
using Repository.Interface;
using Service.Interface;

namespace DependencyRegisterResolver
{
    public class CastleWinsdorRegister : IDependencyRegister<IWindsorContainer>
    {
        private readonly IWindsorContainer _container;

        public CastleWinsdorRegister() : this(null)
        {

        }

        public CastleWinsdorRegister(IWindsorContainer windsorContainer)
        {
            _container = windsorContainer ?? new WindsorContainer();
        }

        public IWindsorContainer Register()
        {
            RegisterRepository();
            RegisterServices();
            RegisterController();

            return _container;
        }

        private void RegisterRepository()
        {
            _container.Register(Component.For<TestDatabaseEntities>().LifestylePerWebRequest());

            _container.Register(Component.For(typeof(IAutoMapperConfigMapper))
                                          .ImplementedBy(typeof(AutoMapperConfigMapper))
                                          .LifestyleSingleton());

            _container.Register(Component.For<ITestRepository>()
                                          .ImplementedBy(typeof(Repository.Implementation.TestRepository))
                                          .LifestylePerWebRequest());                                   
        }

        private void RegisterServices()
        {
            _container.Register(Component.For<IService>()
                                          .ImplementedBy(typeof(Service.Implementation.Service))
                                          .LifestylePerWebRequest());
        }

        private void RegisterController()
        {
            _container.Register(Component.For<IDtoDomainMapper>()
                                          .ImplementedBy(typeof(DtoDomainMapper))
                                          .LifestyleSingleton());

            _container.Register(Component.For<IServiceController>()
                                          .ImplementedBy(typeof(ServiceController))
                                          .LifestylePerWebRequest());
        }
    }
}