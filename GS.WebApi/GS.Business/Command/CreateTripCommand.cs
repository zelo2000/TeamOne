using GS.Business.Infrastructure;
using GS.Business.Infrastructure.Command;
using GS.Business.Mapping;
using GS.Data.Repositories.TripWrite;
using GS.Domain.Models.Trip;
using System.Threading.Tasks;

namespace GS.Business.Command
{
    public class CreateTripCommand : ICommand
    {
        public CreateTripCommand(TripBaseModel trip)
        {
            Trip = trip;
        }

        public TripBaseModel Trip { get; set; }
    }

    public class CreateTripCommandHandler : ICommandHandler<CreateTripCommand>
    {
        private readonly ITripWriteRepository _tripWriteRepository;
        private readonly ITripStatusProvider _tripStatusProvider;

        public CreateTripCommandHandler(ITripWriteRepository tripWriteRepository, ITripStatusProvider tripStatusProvider)
        {
            _tripWriteRepository = tripWriteRepository;
            _tripStatusProvider = tripStatusProvider;
        }

        public async Task Handle(CreateTripCommand command)
        {
            var tripEntity = command.Trip.ToEntity();

            tripEntity.Status = _tripStatusProvider.GetStatus(tripEntity);

            await _tripWriteRepository.CreateTrip(tripEntity);
        }
    }
}
