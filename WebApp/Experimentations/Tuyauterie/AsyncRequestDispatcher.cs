using System;
using System.Threading.Tasks;

namespace WebApp.Experimentations.Tuyauterie
{
    public class AsyncRequestDispatcher : IAsyncRequestDispatcher
    {

        private readonly IServiceRegistry _registry;

        public AsyncRequestDispatcher(IServiceRegistry registry)
        {
            if (registry == null) throw new ArgumentNullException("registry");
            _registry = registry;
        }

        public Task<TResponse> Execute<TRequest, TResponse>(IRequest<TRequest, TResponse> request)
        {
            if (request == null) throw new ArgumentNullException("request");

            // TODO : this cast here works ... but it feels wrong ...
            TRequest castedRequest;
            try
            {
                castedRequest = (TRequest)request;
            }
            catch (InvalidCastException ex)
            {
                throw new RequestDispatcherInvalidTypeException(
                    string.Format("Could not cast the request (of type {0}) to type {1}.\nFor this to work, the request passed to this method should be something like : public class FooRequest : IRequest<FooRequest, FooResponse>. The type is itself the first Type argument...", request.GetType(), typeof(TRequest)), ex);
            }

            IAsyncExecutableService<TRequest, TResponse> service;
            try
            {
                service = _registry.GetAsyncService<TRequest, TResponse>();
            }
            catch (Exception ex)
            {
                throw new RequestDispatcherServiceNotFoundException(string.Format("could not find async service for request of type {0}",
                                                                                  request.GetType()), ex);
            }


            return service.Execute(castedRequest);
        }
    }
}