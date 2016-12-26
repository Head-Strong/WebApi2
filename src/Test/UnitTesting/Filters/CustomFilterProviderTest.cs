using System;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using Controller.Implementation;
using Controller.Implementation.AutoMapperConfigMapper;
using Custom.Filters;
using Moq;
using NUnit.Framework;
using Service.Interface;

namespace UnitTesting.Filters
{
    [TestFixture]
    public class CustomFilterProviderTest
    {
        [Test]
        public void Test()
        {
            var config = new HttpConfiguration();

            var filterProvider = new CustomFilterProvider();

            var mockActionMethodInfo = new ServiceController(new Mock<IService>().Object, new DtoDomainMapper())
                                        .GetType()
                                        .GetMethods(BindingFlags.Public | BindingFlags.Instance)
                                        .FirstOrDefault(x => x.Name.Equals("getdata", StringComparison.OrdinalIgnoreCase));

            var controllerDescriptor = new HttpControllerDescriptor(config, "Service", typeof(ServiceController));

            var actionDescriptor = new ReflectedHttpActionDescriptor(controllerDescriptor, mockActionMethodInfo);

            var result = filterProvider.GetFilters(config, actionDescriptor);

            foreach (var filterInfo in result)
            {
                var data = filterInfo;
            }

        }
    }
}
