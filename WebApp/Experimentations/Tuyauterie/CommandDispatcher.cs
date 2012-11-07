using System;

namespace WebApp.Experimentations.Tuyauterie
{
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
            if (service == null)
            {
                throw new CommandDispatcherServiceNotFoundException(string.Format("could not find service for command of type {0}",
                                                                 command.GetType()));
            }

            return service.Execute(command);
        }
    }
}