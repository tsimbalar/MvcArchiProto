using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Practices.Unity;

namespace WebApp.Experimentations
{
    public class FooCommand : ICommand<FooResponse>
    {
        public string Blob { get; set; }
    }

    public class FooResponse
    {
        public string CapitalizedBlob { get; set; }
    }

    public class BarCommand : ICommand<BarResponse>
    {
        public string Blob { get; set; }
    }

    public class BarResponse
    {
        public string LowerCasedBlob { get; set; }
    }

    // just a "marker" interface ... a command tells us what its return value is
    public interface ICommand<T>
    {
    }

    // responsible for executing a command and returning its result
    public interface ICommandDispatcher
    {
        TResponse Execute<TCommand,TResponse>(TCommand command) where TCommand : class,ICommand<TResponse>;
    }

    public class CommandDispatcher : ICommandDispatcher
    {
        private readonly IServiceRegistry _registry;

        public CommandDispatcher(IServiceRegistry registry)
        {
            if (registry == null) throw new ArgumentNullException("registry");
            _registry = registry;
        }

        public TResponse Execute<TCommand, TResponse>(TCommand command) where TCommand : class,ICommand<TResponse>
        {
            if (command == null) throw new ArgumentNullException("command");

            var service = _registry.GetService<TCommand, TResponse>(command);
            if(service == null)
            {
                throw new ServiceNotFoundException(string.Format("could not find service for command of type {0}",
                                                                 command.GetType()));
            }

            return service.Execute(command);
        }
    }

    public class ServiceNotFoundException : Exception
    {
        public ServiceNotFoundException(string message) : base(message)
        {
        }
    }

    public interface IServiceRegistry
    {
        IExecutableService<TCommand, TResponse> GetService<TCommand, TResponse>(TCommand command) where TCommand : ICommand<TResponse>;

        void Release<TCommand, TResponse>(IExecutableService<TCommand, TResponse> service) where TCommand : ICommand<TResponse>;
    }

    public class UnityServiceRegistry : IServiceRegistry
    {
        private readonly UnityContainer _container;

        public UnityServiceRegistry(UnityContainer container)
        {
            _container = container;
        }

        public IExecutableService<TCommand, TResponse> GetService<TCommand, TResponse>(TCommand command) where TCommand : ICommand<TResponse>
        {
            // whec if it has been registered ...
            if(!_container.IsRegistered<IExecutableService<TCommand, TResponse>>())
            {
                return null;
            }
            var service = _container.Resolve<IExecutableService<TCommand, TResponse>>();

            return service;

        }

        public void Release<TCommand, TResponse>(IExecutableService<TCommand, TResponse> service) where TCommand : ICommand<TResponse>
        {
            _container.Teardown(service);
        }
    }

    public interface IExecutableService<TRequest, TResponse>
    {
        TResponse Execute(TRequest request);
    }

    public class CapitalizationService : IExecutableService<FooCommand, FooResponse>
    {
        public FooResponse Execute(FooCommand request)
        {
            return new FooResponse {CapitalizedBlob = request.Blob.ToUpperInvariant()};
        }
    }
}