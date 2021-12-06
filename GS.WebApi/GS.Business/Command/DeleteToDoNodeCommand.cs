using GS.Business.Infrastructure.Command;
using GS.Data.Repositories.TripWrite;
using System;
using System.Threading.Tasks;

namespace GS.Business.Command
{
    public class DeleteToDoNodeCommand : ICommand
    {
        public DeleteToDoNodeCommand(Guid nodeId)
        {
            NodeId = nodeId;
        }

        public Guid NodeId { get; set; }
    }

    public class DeleteToDoNodeCommandHandler : ICommandHandler<DeleteToDoNodeCommand>
    {
        private readonly ITripWriteRepository _tripWriteRepository;

        public DeleteToDoNodeCommandHandler(ITripWriteRepository tripWriteRepository)
        {
            _tripWriteRepository = tripWriteRepository;
        }

        public async Task Handle(DeleteToDoNodeCommand command)
        {
            await _tripWriteRepository.DeleteToDoNode(command.NodeId);
        }
    }
}
