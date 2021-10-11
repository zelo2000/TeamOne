using GS.Business.Infrastructure;

namespace GS.Business.Command
{
    public class UpdateUserCommand
    {
    }

    public class UpdateUserHandler : ICommandHandler<UpdateUserCommand>
    {
        public void Handle(UpdateUserCommand command)
        {
            throw new System.NotImplementedException();
        }
    }
}
