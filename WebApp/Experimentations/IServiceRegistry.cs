namespace WebApp.Experimentations
{
    public interface IServiceRegistry
    {
        IExecutableService<TCommand, TResponse> GetService<TCommand, TResponse>();

        void Release<TCommand, TResponse>(IExecutableService<TCommand, TResponse> service);
    }
}