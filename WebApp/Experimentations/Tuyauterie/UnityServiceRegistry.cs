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
            // whec if it has been registered ...
            if (!_container.IsRegistered<IExecutableService<TCommand, TResponse>>())
            {
                return null;
            }
            var service = _container.Resolve<IExecutableService<TCommand, TResponse>>();

            return service;

        }

        public void Release<TCommand, TResponse>(IExecutableService<TCommand, TResponse> service)
        {
            _container.Teardown(service);
        }
    }
}