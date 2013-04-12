using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;
using WebApp.Experimentations;
using WebApp.Experimentations.Requests;
using WebApp.Experimentations.Services;
using WebApp.Experimentations.Tuyauterie;

namespace WebAppTest
{
    [TestClass]
    public class AsyncRequestDispatcherTests
    {

        [TestMethod]
        public void AsyncRequestDispatcher_with_FakeServiceRegistry()
        {
            // Arrange
            var serviceRegistry = MockRepository.GenerateStrictMock<IServiceRegistry>();
            serviceRegistry.Stub(s => s.GetAsyncService<CapitalizeRequest, CapitalizeResponse>())
                .Return(new CapitalizationService());

            var sut = new AsyncRequestDispatcher(serviceRegistry);

            var request = new CapitalizeRequest { Blob = "this is lowercase" };

            // Act
            var response = sut.Execute(request).Result;

            // Assert
            Assert.AreEqual("THIS IS LOWERCASE", response.CapitalizedBlob);
        }

        [TestMethod]
        public void AsyncRequestDispatcher_with_Unity_Capitalize()
        {
            // Arrange 
            using (var container = new UnityContainer())
            {
                container.RegisterType<IAsyncExecutableService<CapitalizeRequest, CapitalizeResponse>, CapitalizationService>();

                var serviceRegistry = new UnityServiceRegistry(container);
                var sut = new AsyncRequestDispatcher(serviceRegistry);


                var request = new CapitalizeRequest { Blob = "this is lowercase" };

                // Act
                var response = sut.Execute(request).Result;

                // Assert
                Assert.AreEqual("THIS IS LOWERCASE", response.CapitalizedBlob);
            }
        }

        [TestMethod]
        public void AsyncRequestDispatcher_with_Unity_LowerCasify()
        {
            // Arrange 
            using (var container = new UnityContainer())
            {
                container.RegisterType<IAsyncExecutableService<LowerCasifyRequest, LowerCasifyResponse>, LowerCaseService>();

                var serviceRegistry = new UnityServiceRegistry(container);
                var sut = new AsyncRequestDispatcher(serviceRegistry);


                var request = new LowerCasifyRequest() { Blob = "this is UPPERCASE" };

                // Act
                var response = sut.Execute(request).Result;

                // Assert
                Assert.AreEqual("this is uppercase", response.LowerCasedBlob);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(RequestDispatcherServiceNotFoundException))]
        public void AsyncRequestDispatcher_with_Unity_not_found()
        {
            // Arrange
            using (var container = new UnityContainer())
            {
                container.RegisterType<IAsyncExecutableService<CapitalizeRequest, CapitalizeResponse>, CapitalizationService>();
                var serviceRegistry = new UnityServiceRegistry(container);
                var sut = new AsyncRequestDispatcher(serviceRegistry);

                var request = new LowerCasifyRequest() { Blob = "ThisIsCamelCase" };

                // Act
                sut.Execute(request);
            }
        }
    }
}
