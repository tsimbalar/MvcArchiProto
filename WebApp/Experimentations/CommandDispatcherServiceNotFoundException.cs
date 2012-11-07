namespace WebApp.Experimentations
{
    public class CommandDispatcherServiceNotFoundException : CommandDispatcherException
    {
        public CommandDispatcherServiceNotFoundException(string message)
            : base(message)
        {
        }
    }
}