namespace WebApp.Experimentations.Commands
{

    public class CapitalizeCommand : ICommand<CapitalizeResponse>
    {
        public string Blob { get; set; }
    }

    public class CapitalizeResponse
    {
        public string CapitalizedBlob { get; set; }
    }
}