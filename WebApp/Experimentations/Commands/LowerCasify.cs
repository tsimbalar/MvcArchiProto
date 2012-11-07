namespace WebApp.Experimentations.Commands
{
    public class LowerCasifyCommand : ICommand<LowerCasifyResponse>
    {
        public string Blob { get; set; }
    }

    public class LowerCasifyResponse
    {
        public string LowerCasedBlob { get; set; }
    }
}