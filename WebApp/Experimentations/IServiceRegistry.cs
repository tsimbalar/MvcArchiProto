
namespace WebApp.Experimentations
{
    public interface IServiceRegistry
    {
        IExecutableService<TRequest, TResponse> GetService<TRequest, TResponse>();
        IAsyncExecutableService<TRequest, TResponse> GetAsyncService<TRequest, TResponse>();

        void Release<TRequest, TResponse>(IExecutableService<TRequest, TResponse> service);
        void Release<TRequest, TResponse>(IAsyncExecutableService<TRequest, TResponse> service);
        
    }
}