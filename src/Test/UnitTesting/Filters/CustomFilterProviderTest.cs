using System;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using Controller.Implementation;
using Custom.Filters;
using Moq;
using NUnit.Framework;

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

            var mockActionMethodInfo = new TestController().GetType().GetMethods(BindingFlags.Public | BindingFlags.Instance).FirstOrDefault(x => x.Name.Equals("Get"));

            var controllerDescriptor = new HttpControllerDescriptor(config, "TestController", typeof(TestController));

            var actionDescriptor = new ReflectedHttpActionDescriptor(controllerDescriptor, mockActionMethodInfo);

            var result = filterProvider.GetFilters(config, actionDescriptor);

            foreach (var filterInfo in result)
            {
                var data = filterInfo;
            }

        }       
    }

    public class TestController : ApiController
    {
        public IHttpActionResult Get()
        {
            return null;
        }
    }
}
