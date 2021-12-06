using GS.Business.Infrastructure.Command;
using GS.Business.Mapping;
using GS.Data.Repositories.TripWrite;
using GS.Domain.Models.ToDoNode;
using System;
using System.Threading.Tasks;

namespace GS.Business.Command
{
    public class UpdateToDoNodeCommand : ICommand
    {
        public UpdateToDoNodeCommand(Guid nodeId, ToDoNodeBaseModel node)
        {
            NodeId = nodeId;
            Node = node;
        }

        public Guid NodeId { get; set; }
        public ToDoNodeBaseModel Node { get; set; }
    }

    public class UpdateToDoNodeCommandHandler : ICommandHandler<UpdateToDoNodeCommand>
    {
        private readonly ITripWriteRepository _tripWriteRepository;

        public UpdateToDoNodeCommandHandler(ITripWriteRepository tripWriteRepository)
        {
            _tripWriteRepository = tripWriteRepository;
        }

        public async Task Handle(UpdateToDoNodeCommand command)
        {
            var toDoNode = command.Node.ToEntity();
            await _tripWriteRepository.UpdateToDoNode(command.NodeId, toDoNode);
        }
    }
}
