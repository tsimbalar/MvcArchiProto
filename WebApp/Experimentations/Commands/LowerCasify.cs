using WebApp.Experimentations;

namespace WebApp.Experimentations.Commands
{

    public class LowerCasifyCommand : ICommand<LowerCasifyCommand, LowerCasifyResponse>
    {
        public string Blob { get; set; }
        public LowerCasifyCommand Self { get { return this; } }
    }

    public class LowerCasifyResponse
    {
        public string LowerCasedBlob { get; set; }
    }
}