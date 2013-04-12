using System;
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
    public class RequestDispatcherTests
    {
        [TestMethod]
        public void RequestDispatcher_with_FakeServiceRegistry()
        {
            // Arrange
            var serviceRegistry = MockRepository.GenerateStrictMock<IServiceRegistry>();
            serviceRegistry.Stub(s => s.GetService<CapitalizeRequest, CapitalizeResponse>())
                .Return(new CapitalizationService());

            var sut = new RequestDispatcher(serviceRegistry);

            var request = new CapitalizeRequest { Blob = "this is lowercase" };

            // Act
            var response = sut.Execute(request);


            // Assert
            Assert.AreEqual("THIS IS LOWERCASE", response.CapitalizedBlob);
        }

        [TestMethod]
        public void RequestDispatcher_with_Unity_Capitalize()
        {
            // Arrange 
            using (var container = new UnityContainer())
            {
                container.RegisterType<IExecutableService<CapitalizeRequest, CapitalizeResponse>, CapitalizationService>();

                var serviceRegistry = new UnityServiceRegistry(container);
                var sut = new RequestDispatcher(serviceRegistry);


                var request = new CapitalizeRequest { Blob = "this is lowercase" };

                // Act
                var response = sut.Execute(request);

                // Assert
                Assert.AreEqual("THIS IS LOWERCASE", response.CapitalizedBlob);
            }
        }

        [TestMethod]
        public void RequestDispatcher_with_Unity_LowerCasify()
        {
            // Arrange 
            using (var container = new UnityContainer())
            {
                container.RegisterType<IExecutableService<LowerCasifyRequest, LowerCasifyResponse>, LowerCaseService>();

                var serviceRegistry = new UnityServiceRegistry(container);
                var sut = new RequestDispatcher(serviceRegistry);


                var request = new LowerCasifyRequest() { Blob = "this is UPPERCASE" };

                // Act
                var response = sut.Execute(request);

                // Assert
                Assert.AreEqual("this is uppercase", response.LowerCasedBlob);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(RequestDispatcherServiceNotFoundException))]
        public void RequestDispatcher_with_Unity_not_found()
        {
            // Arrange
            using (var container = new UnityContainer())
            {
                container.RegisterType<IExecutableService<CapitalizeRequest, CapitalizeResponse>, CapitalizationService>();
                var serviceRegistry = new UnityServiceRegistry(container);
                var sut = new RequestDispatcher(serviceRegistry);
                
                var request = new LowerCasifyRequest() { Blob = "ThisIsCamelCase" };

                // Act
                sut.Execute(request);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(RequestDispatcherInvalidTypeException))]
        public void RequestDispatcher_with_wrong_type_of_Request()
        {
            // Arrange
            var serviceRegistry = MockRepository.GenerateStrictMock<IServiceRegistry>();
            var sut = new RequestDispatcher(serviceRegistry);

            var request = new CrappyRequest();

            // Act
            sut.Execute(request);

            // Assert
            // expect exception
        }
    }

    /// <summary>
    /// This won't work because it is not something like MyRequest : IRequest[MyRequest, SomeResponse]
    /// 
    /// </summary>
    class CrappyRequest : IRequest<string, CrappyResponse>
    {

    }

    internal class CrappyResponse
    {
    }
}
