using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Controller.Implementation;
using Controller.Implementation.AutoMapperConfigMapper;
using Controller.Implementation.v1;
using Controller.Interface;
using ORM.Data;
using Repository.Implementation.AutoMapperConfigMapper;
using Repository.Interface;
using Service.Interface;

namespace DependencyRegisterResolver.Register
{
    internal class CastleWinsdorRegister : IDependencyRegister<IWindsorContainer>
    {
        internal CastleWinsdorRegister() : this(null)
        {

        }

        internal CastleWinsdorRegister(IWindsorContainer windsorContainer)
        {
            GetContainer = windsorContainer ?? new WindsorContainer();
        }

        public IWindsorContainer GetContainer { get; }

        public IWindsorContainer Register()
        {
            RegisterRepository();
            RegisterServices();
            RegisterController();

            return GetContainer;
        }

        private void RegisterRepository()
        {
            GetContainer.Register(Component.For<TestDatabaseEntities>().LifestylePerWebRequest());

            GetContainer.Register(Component.For(typeof(IAutoMapperConfigMapper))
                                          .ImplementedBy(typeof(AutoMapperConfigMapper))
                                          .LifestyleSingleton());

            GetContainer.Register(Component.For<ITestRepository>()
                                          .ImplementedBy(typeof(Repository.Implementation.TestRepository))
                                          .LifestylePerWebRequest());                                   
        }

        private void RegisterServices()
        {
            GetContainer.Register(Component.For<IService>()
                                          .ImplementedBy(typeof(Service.Implementation.Service))
                                          .LifestylePerWebRequest());
        }

        private void RegisterController()
        {
            GetContainer.Register(Component.For<IDtoDomainMapper>()
                                          .ImplementedBy(typeof(DtoDomainMapper))
                                          .LifestyleSingleton());

            GetContainer.Register(Component.For<IServiceController>()
                                          .ImplementedBy(typeof(ServiceController))
                                          .LifestylePerWebRequest());
        }
    }
}