using WebApp.Experimentations.Commands;

namespace WebApp.Experimentations.Services
{
    public class CapitalizationService : IExecutableService<CapitalizeCommand, CapitalizeResponse>
    {
        public CapitalizeResponse Execute(CapitalizeCommand request)
        {
            return new CapitalizeResponse { CapitalizedBlob = request.Blob.ToUpperInvariant() };
        }
    }
}