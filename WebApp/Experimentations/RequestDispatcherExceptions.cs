using System;

namespace WebApp.Experimentations
{
    public abstract class RequestDispatcherException : Exception
    {
        protected RequestDispatcherException(string message)
            : base(message)
        {
        }

        protected RequestDispatcherException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }

    public class RequestDispatcherInvalidTypeException : RequestDispatcherException
    {
        public RequestDispatcherInvalidTypeException(string message)
            : base(message)
        {
        }

        public RequestDispatcherInvalidTypeException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }

    public class RequestDispatcherServiceNotFoundException : RequestDispatcherException
    {
        public RequestDispatcherServiceNotFoundException(string message)
            : base(message)
        {
        }

        public RequestDispatcherServiceNotFoundException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}