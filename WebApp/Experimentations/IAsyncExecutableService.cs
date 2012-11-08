using System.Threading.Tasks;

namespace WebApp.Experimentations
{
    public interface IAsyncExecutableService<TRequest, TResponse>
    {
        Task<TResponse> Execute(TRequest request);
    }
}