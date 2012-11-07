using System;
using Microsoft.Practices.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoRhinoMock;
using Rhino.Mocks;
using WebApp.Controllers;
using WebApp.Experimentations;

namespace WebAppTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void disatcher_with_FakeServiceRegistry()
        {
            var serviceRegistry = MockRepository.GenerateStrictMock<IServiceRegistry>();
            serviceRegistry.Stub(s => s.GetService<FooCommand, FooResponse>(null)).IgnoreArguments()
                .Return(new CapitalizationService());

            var commandDispatcher = new CommandDispatcher(serviceRegistry);

            var command = new FooCommand { Blob = "this is lowercase" };

            var response = commandDispatcher.Execute<FooCommand, FooResponse>(command);

            Assert.AreEqual("THIS IS LOWERCASE", response.CapitalizedBlob);
        }

        [TestMethod]
        public void disatcher_with_Unity()
        {
            using (var container = new UnityContainer())
            {


                container.RegisterType<IExecutableService<FooCommand, FooResponse>, CapitalizationService>();


                var serviceRegistry = new UnityServiceRegistry(container);

                var commandDispatcher = new CommandDispatcher(serviceRegistry);

                var command = new FooCommand {Blob = "this is lowercase"};

                var response = commandDispatcher.Execute<FooCommand, FooResponse>(command);

                Assert.AreEqual("THIS IS LOWERCASE", response.CapitalizedBlob);
            }
        }

        [TestMethod]
        [ExpectedException(typeof (ServiceNotFoundException))]
        public void dispatcher_with_Unity_not_found()
        {
            using (var container = new UnityContainer())
            {
                container.RegisterType<IExecutableService<FooCommand, FooResponse>, CapitalizationService>();


                var serviceRegistry = new UnityServiceRegistry(container);

                var commandDispatcher = new CommandDispatcher(serviceRegistry);

                var command = new BarCommand() {Blob = "ThisIsCamelCase"};

                var response = commandDispatcher.Execute<BarCommand, BarResponse>(command);
            }
        }
    }
}
