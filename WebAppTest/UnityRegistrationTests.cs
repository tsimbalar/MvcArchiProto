using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebApp.Experimentations;
using WebApp.Experimentations.Requests;
using WebApp.Experimentations.Services;

namespace WebAppTest
{
    [TestClass]
    public class UnityRegistrationTests
    {
        [TestMethod]
        public void Request_implementations_are_registered()
        {
            // Arrange
            var container = new UnityContainer();
            container.RegisterType<IExecutableService<LowerCasifyRequest, LowerCasifyResponse>, LowerCaseService>();
            container.RegisterType<IExecutableService<CapitalizeRequest, CapitalizeResponse>, CapitalizationService>();
            // Act

            // Assert
            AssertServicesAreRegisteredForAllRequests(container);

        }

        [TestMethod]
        public void AssertServiceForRequestIsRegistered_fails_if_a_service_is_missing()
        {
            // Arrange
            var container = new UnityContainer();
            //container.RegisterType<IExecutableService<LowerCasifyRequest, LowerCasifyResponse>, LowerCaseService>();
            //container.RegisterType<IExecutableService<CapitalizeRequest, CapitalizeResponse>, CapitalizationService>();
            // Act
            AssertFailedException assertionFailure = null;
            
            try
            {
                AssertServicesAreRegisteredForAllRequests(container);
            }catch(AssertFailedException failure)
            {
                assertionFailure = failure;
            }

            // Assert
            Assert.IsNotNull(assertionFailure, "Should have gotten an AssertionException");
            var errormsg = assertionFailure.Message;
            Assert.IsTrue(errormsg.Contains("Executable services are not registered"), "should contain text \"Executable services are not registered\". Actual :\n" + errormsg);
            Assert.IsTrue(errormsg.Contains(typeof(IExecutableService<LowerCasifyRequest, LowerCasifyResponse>).ToString()), "should contain text about service for LowerCase. Actual :\n" + errormsg);
            Assert.IsTrue(errormsg.Contains(typeof(IExecutableService<CapitalizeRequest, CapitalizeResponse>).ToString()), "should contain text about service for Capitalize. Actual :\n" + errormsg);
        }

        private IEnumerable<KeyValuePair<Type, Type>> ListAllExpectedServiceTypesForAllRequests()
        {
            var serviceToRequestCorrespondances =
                typeof(CapitalizeRequest).Assembly.DefinedTypes
                    .Where(IsIRequestImplementation)
                    .Select(cmdType => new KeyValuePair<Type, Type>(GetExpectedServiceTypeForRequest(cmdType), cmdType))
                    .ToList();
            return serviceToRequestCorrespondances;
        } 

        private void AssertServicesAreRegisteredForAllRequests(UnityContainer container)
        {
            var serviceToRequestCorrespondances = ListAllExpectedServiceTypesForAllRequests();

            var notRegisteredServices =
                serviceToRequestCorrespondances.Where(kvp => !container.IsRegistered(kvp.Key)).ToList();

            if(notRegisteredServices.Any())
            {
                var message = notRegisteredServices.Count + 
                    " Executable services are not registered for some Requests and will cause the application to fail when looking up the service for the request: \n" +
                              String.Join("\n", notRegisteredServices.Select(kvp => String.Format("- {0} (for Request {1})", kvp.Key, kvp.Value)));
                Assert.Fail(message);
            }

        }

        private Type GetExpectedServiceTypeForRequest(Type requestType)
        {
            // get the generic interface (IRequest<something,Response>)
            var irequestInterface = requestType.GetInterfaces().Single(f => f.IsInterface && f.IsGenericType && f.Name.StartsWith("IRequest"));
            var responseType = irequestInterface.GetGenericArguments()[1];
            //Assert.Fail(String.Format("{0}", responseType));
            var serviceTypeToFind = typeof(IExecutableService<,>).MakeGenericType(requestType, responseType);
            return serviceTypeToFind;
        }

        private void AssertServiceForRequestIsRegistered(UnityContainer container, Type requestType)
        {
            var serviceTypeToFind = GetExpectedServiceTypeForRequest(requestType);
            Assert.IsTrue(container.IsRegistered(serviceTypeToFind), string.Format("Type {0} should be registered", serviceTypeToFind));
        }

        /// <summary>
        /// Checks that a given type is an implementation of IRequest
        /// </summary>
        private bool IsIRequestImplementation(TypeInfo typeInfo)
        {
            var requestInterface = typeof(IRequest<,>);
            return typeInfo.ImplementedInterfaces.Any(t => t.IsInterface && t.IsGenericType && t.GetGenericTypeDefinition() == requestInterface);
        }
    }
}
