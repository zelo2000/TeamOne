using GS.Business.Infrastructure.Command;
using System;

namespace GS.Business.Command.Core
{
    public class CommandHandlerFactory : ICommandHandlerFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public CommandHandlerFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public ICommandHandler<TCommand> Create<TCommand>(TCommand query) where TCommand : ICommand
        {
            var genericType = typeof(ICommandHandler<>).MakeGenericType(query.GetType());
            return _serviceProvider.GetService(genericType) as ICommandHandler<TCommand>;
        }
    }
}
