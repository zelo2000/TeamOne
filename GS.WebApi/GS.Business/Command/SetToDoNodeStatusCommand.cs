using GS.Business.Infrastructure.Command;
using GS.Data.Repositories.TripWrite;
using GS.Domain.Enums;
using System;
using System.Threading.Tasks;

namespace GS.Business.Command
{
    public class SetToDoNodeStatusCommand : ICommand
    {
        public SetToDoNodeStatusCommand(Guid nodeId, NodeStatus status)
        {
            NodeId = nodeId;
            Status = status;
        }

        public Guid NodeId { get; set; }
        public NodeStatus Status { get; set; }
    }

    public class SetToDoNodeStatusCommandHandler : ICommandHandler<SetToDoNodeStatusCommand>
    {
        private readonly ITripWriteRepository _tripWriteRepository;

        public SetToDoNodeStatusCommandHandler(ITripWriteRepository tripWriteRepository)
        {
            _tripWriteRepository = tripWriteRepository;
        }

        public async Task Handle(SetToDoNodeStatusCommand command)
        {
            await _tripWriteRepository.SetToDoNodeStatus(command.NodeId, command.Status);
        }
    }
}
