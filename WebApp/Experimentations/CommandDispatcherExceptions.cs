using System;

namespace WebApp.Experimentations
{
    public abstract class CommandDispatcherException : Exception
    {
        protected CommandDispatcherException(string message)
            : base(message)
        {
        }

        protected CommandDispatcherException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }

    public class CommandDispatcherInvalidTypeException : CommandDispatcherException
    {
        public CommandDispatcherInvalidTypeException(string message)
            : base(message)
        {
        }

        public CommandDispatcherInvalidTypeException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }

    public class CommandDispatcherServiceNotFoundException : CommandDispatcherException
    {
        public CommandDispatcherServiceNotFoundException(string message)
            : base(message)
        {
        }

        public CommandDispatcherServiceNotFoundException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}