namespace WebApp.Experimentations.Commands
{

    public class CapitalizeCommand : ICommand<CapitalizeCommand, CapitalizeResponse>
    {
        public string Blob { get; set; }
        public CapitalizeCommand Self { get { return this; } }
    }

    public class CapitalizeResponse
    {
        public string CapitalizedBlob { get; set; }
    }
}