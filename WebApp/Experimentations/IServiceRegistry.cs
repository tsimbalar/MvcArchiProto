
namespace WebApp.Experimentations
{
    public interface IServiceRegistry
    {
        IExecutableService<TCommand, TResponse> GetService<TCommand, TResponse>();
        IAsyncExecutableService<TCommand, TResponse> GetAsyncService<TCommand, TResponse>();

        void Release<TCommand, TResponse>(IExecutableService<TCommand, TResponse> service);
        void Release<TCommand, TResponse>(IAsyncExecutableService<TCommand, TResponse> service);
        
    }
}