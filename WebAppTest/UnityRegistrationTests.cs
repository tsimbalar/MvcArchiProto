using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebApp.Experimentations;
using WebApp.Experimentations.Commands;
using WebApp.Experimentations.Services;

namespace WebAppTest
{
    [TestClass]
    public class UnityRegistrationTests
    {
        [TestMethod]
        public void Command_implementations_are_registered()
        {
            // Arrange
            var container = new UnityContainer();
            container.RegisterType<IExecutableService<LowerCasifyCommand, LowerCasifyResponse>, LowerCaseService>();
            container.RegisterType<IExecutableService<CapitalizeCommand, CapitalizeResponse>, CapitalizationService>();
            // Act

            // Assert
            AssertServicesAreRegisteredForAllCommands(container);

        }

        [TestMethod]
        public void AssertServiceForCommandIsRegistered_fails_if_a_service_is_missing()
        {
            // Arrange
            var container = new UnityContainer();
            //container.RegisterType<IExecutableService<LowerCasifyCommand, LowerCasifyResponse>, LowerCaseService>();
            //container.RegisterType<IExecutableService<CapitalizeCommand, CapitalizeResponse>, CapitalizationService>();
            // Act
            AssertFailedException assertionFailure = null;
            
            try
            {
                AssertServicesAreRegisteredForAllCommands(container);
            }catch(AssertFailedException failure)
            {
                assertionFailure = failure;
            }

            // Assert
            Assert.IsNotNull(assertionFailure, "Should have gotten an AssertionException");
            var errormsg = assertionFailure.Message;
            Assert.IsTrue(errormsg.Contains("Executable services are not registered"), "should contain text \"Executable services are not registered\". Actual :\n" + errormsg);
            Assert.IsTrue(errormsg.Contains(typeof(IExecutableService<LowerCasifyCommand, LowerCasifyResponse>).ToString()), "should contain text about service for LowerCase. Actual :\n" + errormsg);
            Assert.IsTrue(errormsg.Contains(typeof(IExecutableService<CapitalizeCommand, CapitalizeResponse>).ToString()), "should contain text about service for Capitalize. Actual :\n" + errormsg);
        }

        private IEnumerable<KeyValuePair<Type, Type>> ListAllExpectedServiceTypesForAllCommands()
        {
            var serviceToCommandCorrespondances =
                typeof(CapitalizeCommand).Assembly.DefinedTypes
                    .Where(IsICommandImplementation)
                    .Select(cmdType => new KeyValuePair<Type, Type>(GetExpectedServiceTypeForCommand(cmdType), cmdType))
                    .ToList();
            return serviceToCommandCorrespondances;
        } 

        private void AssertServicesAreRegisteredForAllCommands(UnityContainer container)
        {
            var serviceToCommandCorrespondances = ListAllExpectedServiceTypesForAllCommands();

            var notRegisteredServices =
                serviceToCommandCorrespondances.Where(kvp => !container.IsRegistered(kvp.Key)).ToList();

            if(notRegisteredServices.Any())
            {
                var message = notRegisteredServices.Count + 
                    " Executable services are not registered for some Commands and will cause the application to fail when looking up the service for the command: \n" +
                              String.Join("\n", notRegisteredServices.Select(kvp => String.Format("- {0} (for Command {1})", kvp.Key, kvp.Value)));
                Assert.Fail(message);
            }

        }

        private Type GetExpectedServiceTypeForCommand(Type commandType)
        {
            // get the generic interface (ICommand<something,Response>)
            var icommandInterface = commandType.GetInterfaces().Single(f => f.IsInterface && f.IsGenericType && f.Name.StartsWith("ICommand"));
            var responseType = icommandInterface.GetGenericArguments()[1];
            //Assert.Fail(String.Format("{0}", responseType));
            var serviceTypeToFind = typeof(IExecutableService<,>).MakeGenericType(commandType, responseType);
            return serviceTypeToFind;
        }

        private void AssertServiceForCommandIsRegistered(UnityContainer container, Type commandType)
        {
            var serviceTypeToFind = GetExpectedServiceTypeForCommand(commandType);
            Assert.IsTrue(container.IsRegistered(serviceTypeToFind), string.Format("Type {0} should be registered", serviceTypeToFind));
        }

        /// <summary>
        /// Checks that a given type is an implementation of ICommand
        /// </summary>
        private bool IsICommandImplementation(TypeInfo typeInfo)
        {
            var commandInterface = typeof(ICommand<,>);
            return typeInfo.ImplementedInterfaces.Any(t => t.IsInterface && t.IsGenericType && t.GetGenericTypeDefinition() == commandInterface);
        }
    }
}
