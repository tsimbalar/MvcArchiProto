namespace WebApp.Experimentations.Commands
{

    public class CapitalizeCommand : ICommand<CapitalizeCommand, CapitalizeResponse>
    {
        public string Blob { get; set; }
    }

    public class CapitalizeResponse
    {
        public string CapitalizedBlob { get; set; }
    }
}