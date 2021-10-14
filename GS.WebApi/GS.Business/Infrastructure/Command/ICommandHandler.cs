using System.Threading.Tasks;

namespace GS.Business.Infrastructure.Command
{
    public interface ICommandHandler<T> where T : ICommand
    {
        public Task Handle(T command);
    }

    public interface ICommandHandler
    {
        Task Handle<TCommand>(TCommand command) where TCommand : ICommand;
    }
}
