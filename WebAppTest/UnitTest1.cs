using System;
using Microsoft.Practices.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoRhinoMock;
using Rhino.Mocks;
using WebApp.Controllers;
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
        public void disatcher_with_FakeServiceRegistry()
        {
            var serviceRegistry = MockRepository.GenerateStrictMock<IServiceRegistry>();
            serviceRegistry.Stub(s => s.GetService<CapitalizeCommand, CapitalizeResponse>(null)).IgnoreArguments()
                .Return(new CapitalizationService());

            var commandDispatcher = new CommandDispatcher(serviceRegistry);

            var command = new CapitalizeCommand { Blob = "this is lowercase" };

            var response = commandDispatcher.Execute<CapitalizeCommand, CapitalizeResponse>(command);

            Assert.AreEqual("THIS IS LOWERCASE", response.CapitalizedBlob);
        }

        [TestMethod]
        public void disatcher_with_Unity()
        {
            using (var container = new UnityContainer())
            {


                container.RegisterType<IExecutableService<CapitalizeCommand, CapitalizeResponse>, CapitalizationService>();


                var serviceRegistry = new UnityServiceRegistry(container);

                var commandDispatcher = new CommandDispatcher(serviceRegistry);

                var command = new CapitalizeCommand {Blob = "this is lowercase"};

                var response = commandDispatcher.Execute<CapitalizeCommand, CapitalizeResponse>(command);

                Assert.AreEqual("THIS IS LOWERCASE", response.CapitalizedBlob);
            }
        }

        [TestMethod]
        [ExpectedException(typeof (CommandDispatcherServiceNotFoundException))]
        public void dispatcher_with_Unity_not_found()
        {
            using (var container = new UnityContainer())
            {
                container.RegisterType<IExecutableService<CapitalizeCommand, CapitalizeResponse>, CapitalizationService>();


                var serviceRegistry = new UnityServiceRegistry(container);

                var commandDispatcher = new CommandDispatcher(serviceRegistry);

                var command = new LowerCasifyCommand() {Blob = "ThisIsCamelCase"};

                commandDispatcher.Execute<LowerCasifyCommand, LowerCasifyResponse>(command);
            }
        }
    }
}
