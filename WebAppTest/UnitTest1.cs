using System;
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
    public class UnitTest1
    {
        [TestMethod]
        public void dispatcher_with_FakeServiceRegistry()
        {
            // Arrange
            var serviceRegistry = MockRepository.GenerateStrictMock<IServiceRegistry>();
            serviceRegistry.Stub(s => s.GetService<CapitalizeCommand, CapitalizeResponse>())
                .Return(new CapitalizationService());

            var commandDispatcher = new CommandDispatcher(serviceRegistry);

            var command = new CapitalizeCommand { Blob = "this is lowercase" };

            // Act
            var response = commandDispatcher.Execute(command);


            // Assert
            Assert.AreEqual("THIS IS LOWERCASE", response.CapitalizedBlob);
        }

        [TestMethod]
        public void dispatcher_with_Unity()
        {
            // Arrange 
            using (var container = new UnityContainer())
            {
                container.RegisterType<IExecutableService<CapitalizeCommand, CapitalizeResponse>, CapitalizationService>();

                var serviceRegistry = new UnityServiceRegistry(container);
                var commandDispatcher = new CommandDispatcher(serviceRegistry);


                var command = new CapitalizeCommand { Blob = "this is lowercase" };

                // Act
                var response = commandDispatcher.Execute(command);

                // Assert
                Assert.AreEqual("THIS IS LOWERCASE", response.CapitalizedBlob);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(CommandDispatcherServiceNotFoundException))]
        public void dispatcher_with_Unity_not_found()
        {
            // Arrange
            using (var container = new UnityContainer())
            {
                container.RegisterType<IExecutableService<CapitalizeCommand, CapitalizeResponse>, CapitalizationService>();
                var serviceRegistry = new UnityServiceRegistry(container);
                var commandDispatcher = new CommandDispatcher(serviceRegistry);
                
                var command = new LowerCasifyCommand() { Blob = "ThisIsCamelCase" };

                // Act
                commandDispatcher.Execute(command);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(CommandDispatcherInvalidTypeException))]
        public void dispatcher_with_wrong_type_of_command()
        {
            // Arrange
            var serviceRegistry = MockRepository.GenerateStrictMock<IServiceRegistry>();
            var commandDispatcher = new CommandDispatcher(serviceRegistry);

            var command = new CrappyCommand();

            // Act
            commandDispatcher.Execute(command);

            // Assert
            // expect exception
        }
    }


    class CrappyCommand : ICommand<string, CrappyResponse>
    {

    }

    internal class CrappyResponse
    {
    }
}
