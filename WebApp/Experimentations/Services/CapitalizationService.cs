using System.Threading.Tasks;
using WebApp.Experimentations.Commands;

namespace WebApp.Experimentations.Services
{
    public class CapitalizationService : IExecutableService<CapitalizeCommand, CapitalizeResponse>, IAsyncExecutableService<CapitalizeCommand, CapitalizeResponse>
    {
        public CapitalizeResponse Execute(CapitalizeCommand request)
        {
            return new CapitalizeResponse { CapitalizedBlob = request.Blob.ToUpperInvariant() };
        }

        Task<CapitalizeResponse> IAsyncExecutableService<CapitalizeCommand, CapitalizeResponse>.Execute(CapitalizeCommand request)
        {
            return Task.FromResult(this.Execute(request));
        }
    }
}