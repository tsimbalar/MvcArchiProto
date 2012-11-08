using System.Threading.Tasks;

namespace WebApp.Experimentations
{
    /// <summary>
    /// responsible for executing a command and returning its result
    /// </summary>
    public interface ICommandDispatcher
    {
        TResponse Execute<TCommand, TResponse>(ICommand<TCommand, TResponse> command);
    }

    public interface IAsyncCommandDispatcher
    {
        Task<TResponse> Execute<TCommand, TResponse>(ICommand<TCommand, TResponse> command);
    }
}