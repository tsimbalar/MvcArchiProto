using System.Threading.Tasks;

namespace WebApp.Experimentations
{
    /// <summary>
    /// responsible for executing a request and returning its result
    /// </summary>
    public interface IAsyncRequestDispatcher
    {
        Task<TResponse> Execute<TRequest, TResponse>(IRequest<TRequest, TResponse> request);
    }
}