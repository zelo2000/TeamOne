using GS.Business.Infrastructure;
using GS.Business.Infrastructure.Command;
using GS.Data.Repositories.TripWrite;
using System;
using System.Threading.Tasks;

namespace GS.Business.Command
{
    public class DeleteTripCommand : ICommand
    {
        public DeleteTripCommand(Guid tripId)
        {
            TripId = tripId;
        }

        public Guid TripId { get; set; }
    }

    public class DeleteTripCommandHandler : ICommandHandler<DeleteTripCommand>
    {
        private readonly ITripWriteRepository _tripWriteRepository;

        public DeleteTripCommandHandler(ITripWriteRepository tripWriteRepository)
        {
            _tripWriteRepository = tripWriteRepository;
        }

        public async Task Handle(DeleteTripCommand command)
        {
            await _tripWriteRepository.DeleteTrip(command.TripId);
        }
    }
}
