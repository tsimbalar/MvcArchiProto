namespace WebApp.Experimentations.Requests
{

    public class LowerCasifyRequest : IRequest<LowerCasifyRequest, LowerCasifyResponse>
    {
        public string Blob { get; set; }
    }

    public class LowerCasifyResponse
    {
        public string LowerCasedBlob { get; set; }
    }
}