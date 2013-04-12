using System.Threading.Tasks;
using WebApp.Experimentations.Requests;

namespace WebApp.Experimentations.Services
{
    public class LowerCaseService : IExecutableService<LowerCasifyRequest, LowerCasifyResponse>, IAsyncExecutableService<LowerCasifyRequest, LowerCasifyResponse>
    {
        public LowerCasifyResponse Execute(LowerCasifyRequest request)
        {
            return new LowerCasifyResponse{ LowerCasedBlob = request.Blob.ToLowerInvariant() };
        }

        Task<LowerCasifyResponse> IAsyncExecutableService<LowerCasifyRequest, LowerCasifyResponse>.Execute(LowerCasifyRequest request)
        {
            return Task.FromResult(Execute(request));
        }
    }
}