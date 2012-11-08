using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;
using WebApp.Experimentations;
using WebApp.Experimentations.Commands;
using WebApp.Experimentations.Services;
using WebApp.Experimentations.Tuyauterie;

namespace WebAppTest
{
    [TestClass]
    public class AsyncCommandDispatcherTests
    {

        [TestMethod]
        public void AsyncCommandDispatcher_with_FakeServiceRegistry()
        {
            // Arrange
            var serviceRegistry = MockRepository.GenerateStrictMock<IServiceRegistry>();
            serviceRegistry.Stub(s => s.GetAsyncService<CapitalizeCommand, CapitalizeResponse>())
                .Return(new CapitalizationService());

            var commandDispatcher = new AsyncCommandDispatcher(serviceRegistry);

            var command = new CapitalizeCommand { Blob = "this is lowercase" };

            // Act
            var response = commandDispatcher.Execute(command).Result;

            // Assert
            Assert.AreEqual("THIS IS LOWERCASE", response.CapitalizedBlob);
        }

        [TestMethod]
        public void AsyncCommandDispatcher_with_Unity_Capitalize()
        {
            // Arrange 
            using (var container = new UnityContainer())
            {
                container.RegisterType<IAsyncExecutableService<CapitalizeCommand, CapitalizeResponse>, CapitalizationService>();

                var serviceRegistry = new UnityServiceRegistry(container);
                var commandDispatcher = new AsyncCommandDispatcher(serviceRegistry);


                var command = new CapitalizeCommand { Blob = "this is lowercase" };

                // Act
                var response = commandDispatcher.Execute(command).Result;

                // Assert
                Assert.AreEqual("THIS IS LOWERCASE", response.CapitalizedBlob);
            }
        }

        [TestMethod]
        public void AsyncCommandDispatcher_with_Unity_LowerCasify()
        {
            // Arrange 
            using (var container = new UnityContainer())
            {
                container.RegisterType<IAsyncExecutableService<LowerCasifyCommand, LowerCasifyResponse>, LowerCaseService>();

                var serviceRegistry = new UnityServiceRegistry(container);
                var commandDispatcher = new AsyncCommandDispatcher(serviceRegistry);


                var command = new LowerCasifyCommand() { Blob = "this is UPPERCASE" };

                // Act
                var response = commandDispatcher.Execute(command).Result;

                // Assert
                Assert.AreEqual("this is uppercase", response.LowerCasedBlob);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(CommandDispatcherServiceNotFoundException))]
        public void AsyncCommandDispatcher_with_Unity_not_found()
        {
            // Arrange
            using (var container = new UnityContainer())
            {
                container.RegisterType<IAsyncExecutableService<CapitalizeCommand, CapitalizeResponse>, CapitalizationService>();
                var serviceRegistry = new UnityServiceRegistry(container);
                var commandDispatcher = new AsyncCommandDispatcher(serviceRegistry);

                var command = new LowerCasifyCommand() { Blob = "ThisIsCamelCase" };

                // Act
                commandDispatcher.Execute(command);
            }
        }
    }
}
