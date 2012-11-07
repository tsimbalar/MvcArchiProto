namespace WebApp.Experimentations.Commands
{

    public class FooCommand : ICommand<FooResponse>
    {
        public string Blob { get; set; }
    }

    public class FooResponse
    {
        public string CapitalizedBlob { get; set; }
    }
}