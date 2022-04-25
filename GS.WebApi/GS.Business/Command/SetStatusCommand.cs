using GS.Business.Infrastructure;
using GS.Business.Infrastructure.Command;
using GS.Data.Repositories.TripRead;
using GS.Data.Repositories.TripWrite;
using System;
using System.Threading.Tasks;

namespace GS.Business.Command
{
    public class SetStatusCommand : ICommand
    {
        public SetStatusCommand(Guid tripId)
        {
            TripId = tripId;
        }

        public Guid TripId { get; set; }
    }

    public class SetStatusCommandHandler : ICommandHandler<SetStatusCommand>
    {
        private readonly ITripWriteRepository _tripWriteRepository;
        private readonly ITripReadRepository _tripReadRepository;
        private readonly ITripStatusProvider _tripStatusProvider;

        public SetStatusCommandHandler(ITripWriteRepository tripWriteRepository, ITripReadRepository tripReadRepository, ITripStatusProvider tripStatusProvider)
        {
            _tripWriteRepository = tripWriteRepository;
            _tripReadRepository = tripReadRepository;
            _tripStatusProvider = tripStatusProvider;
        }

        public async Task Handle(SetStatusCommand command)
        {
            var trip = await _tripReadRepository.GetTripById(command.TripId);
            var status = _tripStatusProvider.GetStatus(trip);
            await _tripWriteRepository.SetTripStatus(command.TripId, status);
        }
    }
}
