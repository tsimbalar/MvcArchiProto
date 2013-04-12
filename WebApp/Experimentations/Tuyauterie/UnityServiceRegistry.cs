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

        public IExecutableService<TRequest, TResponse> GetService<TRequest, TResponse>()
        {
            // check if it has been registered ...
            try
            {
                var service = _container.Resolve<IExecutableService<TRequest, TResponse>>();

                return service;
            }catch(ResolutionFailedException ex)
            {
                throw new ServiceNotFoundException(string.Format("Could not find IExecutableService service for request type {0} and response type {1}", typeof(TRequest), typeof(TResponse)), ex);
            }
        }

        public IAsyncExecutableService<TRequest, TResponse> GetAsyncService<TRequest, TResponse>()
        {
            // check if it has been registered ...
            try
            {
                var service = _container.Resolve<IAsyncExecutableService<TRequest, TResponse>>();

                return service;
            }
            catch (ResolutionFailedException ex)
            {
                throw new ServiceNotFoundException(string.Format("Could not find IAsyncExecutableService service for request type {0} and response type {1}", typeof(TRequest), typeof(TResponse)), ex);
            }
        }

        public void Release<TRequest, TResponse>(IExecutableService<TRequest, TResponse> service)
        {
            _container.Teardown(service);
        }

        public void Release<TRequest, TResponse>(IAsyncExecutableService<TRequest, TResponse> service)
        {
            _container.Teardown(service);
        }
    }
}