using System.Threading.Tasks;
using WebApp.Experimentations.Commands;

namespace WebApp.Experimentations.Services
{
    public class LowerCaseService : IExecutableService<LowerCasifyCommand, LowerCasifyResponse>, IAsyncExecutableService<LowerCasifyCommand, LowerCasifyResponse>
    {
        public LowerCasifyResponse Execute(LowerCasifyCommand request)
        {
            return new LowerCasifyResponse{ LowerCasedBlob = request.Blob.ToLowerInvariant() };
        }

        Task<LowerCasifyResponse> IAsyncExecutableService<LowerCasifyCommand, LowerCasifyResponse>.Execute(LowerCasifyCommand request)
        {
            return Task.FromResult(Execute(request));
        }
    }
}