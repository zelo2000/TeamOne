using GS.Business.Infrastructure.Command;
using GS.Business.Mapping;
using GS.Data.Repositories.TripWrite;
using GS.Domain.Models.ItemToTake;
using System;
using System.Threading.Tasks;

namespace GS.Business.Command
{
    public class UpdateItemCommand : ICommand
    {
        public UpdateItemCommand(Guid itemId, ItemToTakeBaseModel item)
        {
            ItemId = itemId;
            Item = item;
        }

        public Guid ItemId { get; set; }
        public ItemToTakeBaseModel Item { get; set; }
    }

    public class UpdateItemCommandHandler : ICommandHandler<UpdateItemCommand>
    {
        private readonly ITripWriteRepository _tripWriteRepository;

        public UpdateItemCommandHandler(ITripWriteRepository tripWriteRepository)
        {
            _tripWriteRepository = tripWriteRepository;
        }

        public async Task Handle(UpdateItemCommand command)
        {
            var item = command.Item.ToEntity();
            await _tripWriteRepository.UpdateItem(command.ItemId, item);
        }
    }
}
