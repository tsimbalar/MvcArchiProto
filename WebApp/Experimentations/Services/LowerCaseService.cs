using WebApp.Experimentations.Commands;

namespace WebApp.Experimentations.Services
{
    public class LowerCaseService : IExecutableService<LowerCasifyCommand, LowerCasifyResponse>
    {
        public LowerCasifyResponse Execute(LowerCasifyCommand request)
        {
            return new LowerCasifyResponse{ LowerCasedBlob = request.Blob.ToLowerInvariant() };
        }
    }
}