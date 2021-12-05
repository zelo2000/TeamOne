using GS.Business.Infrastructure.Command;
using GS.Data.Repositories.TripWrite;
using System;
using System.Threading.Tasks;

namespace GS.Business.Command
{
    public class SetIsItemTakenCommand : ICommand
    {
        public SetIsItemTakenCommand(Guid itemId, bool isTaken)
        {
            ItemId = itemId;
            IsTaken = isTaken;
        }

        public Guid ItemId { get; set; }
        public bool IsTaken { get; set; }
    }

    public class SetIsItemTakenCommandHandler : ICommandHandler<SetIsItemTakenCommand>
    {
        private readonly ITripWriteRepository _tripWriteRepository;

        public SetIsItemTakenCommandHandler(ITripWriteRepository tripWriteRepository)
        {
            _tripWriteRepository = tripWriteRepository;
        }

        public async Task Handle(SetIsItemTakenCommand command)
        {
            await _tripWriteRepository.SetIsItemTaken(command.ItemId, command.IsTaken);
        }
    }
}
