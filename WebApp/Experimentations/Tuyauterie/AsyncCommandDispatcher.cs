using System;
using System.Threading.Tasks;

namespace WebApp.Experimentations.Tuyauterie
{
    public class AsyncCommandDispatcher : IAsyncCommandDispatcher
    {

        private readonly IServiceRegistry _registry;

        public AsyncCommandDispatcher(IServiceRegistry registry)
        {
            if (registry == null) throw new ArgumentNullException("registry");
            _registry = registry;
        }

        public Task<TResponse> Execute<TCommand, TResponse>(ICommand<TCommand, TResponse> command)
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

            IAsyncExecutableService<TCommand, TResponse> service;
            try
            {
                service = _registry.GetAsyncService<TCommand, TResponse>();
            }
            catch (Exception ex)
            {
                throw new CommandDispatcherServiceNotFoundException(string.Format("could not find async service for command of type {0}",
                                                                                  command.GetType()), ex);
            }


            return service.Execute(castedCommand);
        }
    }
}