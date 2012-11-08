using System;

namespace WebApp.Experimentations.Tuyauterie
{
    public class ServiceRegistryException : Exception
    {
        public ServiceRegistryException(string message) : base(message)
        {
        }

        public ServiceRegistryException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }

    public class ServiceNotFoundException : ServiceRegistryException
    {
        public ServiceNotFoundException(string message)
            : base(message)
        {
        }

        public ServiceNotFoundException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}