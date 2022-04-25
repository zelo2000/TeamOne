using GS.Business.Command;
using GS.Data.Repositories.TripRead;
using Microsoft.Extensions.Hosting;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GS.Business.Services
{
    class TripStatusChecker : IHostedService, IDisposable
    {
        private Timer _timer;
        private readonly SetStatusCommandHandler _setStatusCommandHandler;
        private readonly ITripReadRepository _tripReadRepository;

        public TripStatusChecker(SetStatusCommandHandler setStatusCommandHandler, ITripReadRepository tripReadRepository)
        {
            _setStatusCommandHandler = setStatusCommandHandler;
            _tripReadRepository = tripReadRepository;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromHours(12));

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        private void DoWork(object state)
        {
            var tripIds = _tripReadRepository.GetTripList().Select(trip => _setStatusCommandHandler.Handle(new SetStatusCommand(trip.Id)));
            Task.WaitAll(tripIds.ToArray());
        }
    }
}
