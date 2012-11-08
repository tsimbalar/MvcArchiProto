using System;

namespace WebApp.Experimentations.Tuyauterie
{
    public class ServiceNotFoundException : ServiceRegistryException
    {
        public ServiceNotFoundException(string message) : base(message)
        {
        }

        public ServiceNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}