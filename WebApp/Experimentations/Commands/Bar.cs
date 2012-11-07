namespace WebApp.Experimentations.Commands
{
    public class BarCommand : ICommand<BarResponse>
    {
        public string Blob { get; set; }
    }

    public class BarResponse
    {
        public string LowerCasedBlob { get; set; }
    }
}