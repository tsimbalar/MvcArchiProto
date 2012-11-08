using System;
using Microsoft.Practices.Unity;

namespace WebApp.Experimentations.Tuyauterie
{
    public class UnityServiceRegistry : IServiceRegistry
    {
        private readonly UnityContainer _container;

        public UnityServiceRegistry(UnityContainer container)
        {
            _container = container;
        }

        public IExecutableService<TCommand, TResponse> GetService<TCommand, TResponse>()
        {
            // check if it has been registered ...
            try
            {
                var service = _container.Resolve<IExecutableService<TCommand, TResponse>>();

                return service;
            }catch(ResolutionFailedException ex)
            {
                throw new ServiceNotFoundException(string.Format("Could not find IExecutableService service for command type {0} and response type {1}", typeof(TCommand), typeof(TResponse)), ex);
            }
        }

        public IAsyncExecutableService<TCommand, TResponse> GetAsyncService<TCommand, TResponse>()
        {
            // check if it has been registered ...
            try
            {
                var service = _container.Resolve<IAsyncExecutableService<TCommand, TResponse>>();

                return service;
            }
            catch (ResolutionFailedException ex)
            {
                throw new ServiceNotFoundException(string.Format("Could not find IAsyncExecutableService service for command type {0} and response type {1}", typeof(TCommand), typeof(TResponse)), ex);
            }
        }

        public void Release<TCommand, TResponse>(IExecutableService<TCommand, TResponse> service)
        {
            _container.Teardown(service);
        }

        public void Release<TCommand, TResponse>(IAsyncExecutableService<TCommand, TResponse> service)
        {
            _container.Teardown(service);
        }
    }

    public class ServiceRegistryException : Exception
    {
        public ServiceRegistryException(string message) : base(message)
        {
        }

        public ServiceRegistryException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}