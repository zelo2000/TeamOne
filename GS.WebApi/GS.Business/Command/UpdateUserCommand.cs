using GS.Business.Infrastructure.Command;
using System.Threading.Tasks;

namespace GS.Business.Command
{
    public class UpdateUserCommand : ICommand
    {
    }

    public class UpdateUserHandler : ICommandHandler<UpdateUserCommand>
    {
        public Task Handle(UpdateUserCommand command)
        {
            throw new System.NotImplementedException();
        }
    }
}
