using GS.Business.Infrastructure.Command;
using GS.Data.Repositories.TripWrite;
using GS.Domain.Models.Trip;
using System;
using System.Threading.Tasks;

namespace GS.Business.Command
{
    public class UpdateTripCommand : ICommand
    {
        public UpdateTripCommand(Guid tripId, TripBaseModel trip)
        {
            TripId = tripId;
            Trip = trip;
        }
        public Guid TripId { get; set; }
        public TripBaseModel Trip { get; set; }
    }

    public class UpdateTripCommandHandler : ICommandHandler<UpdateTripCommand>
    {
        private readonly ITripWriteRepository _tripWriteRepository;

        public UpdateTripCommandHandler(ITripWriteRepository tripWriteRepository)
        {
            _tripWriteRepository = tripWriteRepository;
        }

        public async Task Handle(UpdateTripCommand command)
        {
            await _tripWriteRepository.UpdateTrip(command.TripId, command.Trip);
        }
    }
}
