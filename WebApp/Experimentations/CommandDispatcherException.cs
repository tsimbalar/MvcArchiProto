using System;

namespace WebApp.Experimentations
{
    public abstract class CommandDispatcherException : Exception
    {
        protected CommandDispatcherException(string message)
            : base(message)
        {
        }

        protected CommandDispatcherException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}