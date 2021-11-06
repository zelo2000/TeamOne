using GS.Business.Infrastructure.Command;
using GS.Business.Mapping;
using GS.Data.Repositories.TripWrite;
using GS.Domain.Enums;
using GS.Domain.Models.ToDoNode;
using System;
using System.Threading.Tasks;

namespace GS.Business.Command
{
    public class AddToDoNodeCommand : ICommand
    {
        public AddToDoNodeCommand(Guid tripId, ToDoNodeBaseModel node)
        {
            TripId = tripId;
            Node = node;
        }

        public Guid TripId { get; set; }
        public ToDoNodeBaseModel Node { get; set; }
    }

    public class AddToDoNodeCommandHandler : ICommandHandler<AddToDoNodeCommand>
    {
        private readonly ITripWriteRepository _tripWriteRepository;

        public AddToDoNodeCommandHandler(ITripWriteRepository tripWriteRepository)
        {
            _tripWriteRepository = tripWriteRepository;
        }

        public async Task Handle(AddToDoNodeCommand command)
        {
            var toDoNode = command.Node.ToEntity();

            toDoNode.Id = Guid.NewGuid();
            toDoNode.Status = NodeStatus.ToDo;

            await _tripWriteRepository.AddToDoNode(command.TripId, toDoNode);
        }
    }
}
