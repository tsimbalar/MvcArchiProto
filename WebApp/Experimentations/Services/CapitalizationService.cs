using WebApp.Experimentations.Commands;

namespace WebApp.Experimentations.Services
{
    public class CapitalizationService : IExecutableService<FooCommand, FooResponse>
    {
        public FooResponse Execute(FooCommand request)
        {
            return new FooResponse { CapitalizedBlob = request.Blob.ToUpperInvariant() };
        }
    }
}