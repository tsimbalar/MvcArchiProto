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

        public TResponse Execute<TCommand, TResponse>(ICommand<TCommand, TResponse> command)
        {
            if (command == null) throw new ArgumentNullException("command");

            // TODO : this cast here works ... but it feels wrong ...
            TCommand castedCommand;
            try
            {
                castedCommand = (TCommand)command;
            }
            catch (InvalidCastException ex)
            {
                throw new CommandDispatcherInvalidTypeException(
                    string.Format("Could not cast the command (of type {0}) to type {1}.\nFor this to work, the command passed to this method should be something like : public class FooCommand : ICommand<FooCommand, FooResponse>. The type is itself the first Type argument...", command.GetType(), typeof(TCommand)), ex);
            }

            var service = _registry.GetService<TCommand, TResponse>();
            if (service == null)
            {
                throw new CommandDispatcherServiceNotFoundException(string.Format("could not find service for command of type {0}",
                                                                 typeof(TCommand)));
            }

            
            return service.Execute(castedCommand);
        }
    }
}