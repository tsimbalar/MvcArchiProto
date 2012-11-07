using System;

namespace WebApp.Experimentations
{
    public abstract class CommandDispatcherException : Exception
    {
        protected CommandDispatcherException(string message)
            : base(message)
        {
        }
    }
}