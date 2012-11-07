namespace WebApp.Experimentations
{
    /// <summary>
    /// responsible for executing a command and returning its result
    /// </summary>
    public interface ICommandDispatcher
    {
        TResponse Execute<TCommand, TResponse>(ICommand<TCommand, TResponse> command);
    }
}