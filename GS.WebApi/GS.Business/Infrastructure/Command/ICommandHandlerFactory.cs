namespace GS.Business.Infrastructure.Command
{
    public interface ICommandHandlerFactory
    {
        ICommandHandler<TCommand> Create<TCommand>(TCommand command) where TCommand : ICommand;
    }
}
