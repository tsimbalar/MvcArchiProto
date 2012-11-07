namespace WebApp.Experimentations
{
    /// <summary>
    /// responsible for executing a command and returning its result
    /// </summary>
    public interface ICommandDispatcher
    {
        TResponse Execute<TCommand, TResponse>(TCommand command) where TCommand : class,ICommand<TResponse>;
    }
}