using System;

namespace WebApp.Experimentations
{
    public class CommandDispatcherInvalidTypeException : CommandDispatcherException
    {
        public CommandDispatcherInvalidTypeException(string message)
            : base(message)
        {
        }

        public CommandDispatcherInvalidTypeException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}