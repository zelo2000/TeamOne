using GS.Business.Infrastructure.Command;
using System.Threading.Tasks;

namespace GS.Business.Command.Core
{
    public class CommandHandler : ICommandHandler
    {
        private readonly ICommandHandlerFactory _commandHandlerFactory;

        public CommandHandler(ICommandHandlerFactory commandHandlerFactory)
        {
            _commandHandlerFactory = commandHandlerFactory;
        }

        public Task Handle<TCommand>(TCommand command) where TCommand : ICommand
        {
            var handler = _commandHandlerFactory.Create(command);
            return handler.Handle(command);
        }
    }
}
