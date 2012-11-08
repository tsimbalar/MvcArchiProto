using System;

namespace WebApp.Experimentations
{
    public class CommandDispatcherServiceNotFoundException : CommandDispatcherException
    {
        public CommandDispatcherServiceNotFoundException(string message)
            : base(message)
        {
        }

        public CommandDispatcherServiceNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}