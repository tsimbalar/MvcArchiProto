using WebApp.Experimentations.Commands;

namespace WebApp.Experimentations.Services
{
    public class LowerCaseService : IExecutableService<BarCommand, BarResponse>
    {
        public BarResponse Execute(BarCommand request)
        {
            return new BarResponse{ LowerCasedBlob = request.Blob.ToLowerInvariant() };
        }
    }
}