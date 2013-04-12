using System.Threading.Tasks;
using WebApp.Experimentations.Requests;

namespace WebApp.Experimentations.Services
{
    public class CapitalizationService : IExecutableService<CapitalizeRequest, CapitalizeResponse>, IAsyncExecutableService<CapitalizeRequest, CapitalizeResponse>
    {
        public CapitalizeResponse Execute(CapitalizeRequest request)
        {
            return new CapitalizeResponse { CapitalizedBlob = request.Blob.ToUpperInvariant() };
        }

        Task<CapitalizeResponse> IAsyncExecutableService<CapitalizeRequest, CapitalizeResponse>.Execute(CapitalizeRequest request)
        {
            return Task.FromResult(this.Execute(request));
        }
    }
}