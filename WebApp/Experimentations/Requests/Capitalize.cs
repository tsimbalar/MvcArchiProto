namespace WebApp.Experimentations.Requests
{

    public class CapitalizeRequest : IRequest<CapitalizeRequest, CapitalizeResponse>
    {
        public string Blob { get; set; }
    }

    public class CapitalizeResponse
    {
        public string CapitalizedBlob { get; set; }
    }
}