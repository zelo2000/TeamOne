using GS.Business.Infrastructure.Command;
using GS.Business.Mapping;
using GS.Data.Repositories.TripWrite;
using System;
using System.Threading.Tasks;
using GS.Domain.Models.ItemToTake;

namespace GS.Business.Command
{
    public class AddItemCommand : ICommand
    {
        public AddItemCommand(Guid tripId, ItemToTakeBaseModel item)
        {
            TripId = tripId;
            Item = item;
        }

        public Guid TripId { get; set; }
        public ItemToTakeBaseModel Item { get; set; }
    }

    public class AddItemCommandHandler : ICommandHandler<AddItemCommand>
    {
        private readonly ITripWriteRepository _tripWriteRepository;

        public AddItemCommandHandler(ITripWriteRepository tripWriteRepository)
        {
            _tripWriteRepository = tripWriteRepository;
        }

        public async Task Handle(AddItemCommand command)
        {
            var item = command.Item.ToEntity();

            item.Id = Guid.NewGuid();
            item.IsTaken = false;

            await _tripWriteRepository.AddItem(command.TripId, item);
        }
    }
}
