using GS.Business.Infrastructure.Command;
using GS.Data.Repositories.TripWrite;
using System;
using System.Threading.Tasks;

namespace GS.Business.Command
{
    public class DeleteItemCommand : ICommand
    {
        public DeleteItemCommand(Guid itemId)
        {
            ItemId = itemId;
        }

        public Guid ItemId { get; set; }
    }

    public class DeleteItemCommandHandler : ICommandHandler<DeleteItemCommand>
    {
        private readonly ITripWriteRepository _tripWriteRepository;

        public DeleteItemCommandHandler(ITripWriteRepository tripWriteRepository)
        {
            _tripWriteRepository = tripWriteRepository;
        }

        public async Task Handle(DeleteItemCommand command)
        {
            await _tripWriteRepository.DeleteItem(command.ItemId);
        }
    }
}
