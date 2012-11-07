namespace WebApp.Experimentations
{
    public interface IServiceRegistry
    {
        IExecutableService<TCommand, TResponse> GetService<TCommand, TResponse>(TCommand command) where TCommand : ICommand<TResponse>;

        void Release<TCommand, TResponse>(IExecutableService<TCommand, TResponse> service) where TCommand : ICommand<TResponse>;
    }
}