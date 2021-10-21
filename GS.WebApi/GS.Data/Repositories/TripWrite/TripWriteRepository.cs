using GS.Data.Entities;
using System.Threading.Tasks;

namespace GS.Data.Repositories.TripWrite
{
    public class TripWriteRepository : ITripWriteRepository
    {
        private readonly TripDbContext _tripDbContext;

        public TripWriteRepository(TripDbContext tripDbContext)
        {
            _tripDbContext = tripDbContext;
        }

        public async Task CreateTrip(Trip trip)
        {
            if (trip.StartDate.HasValue)
                trip.StartDate = trip.StartDate.Value.ToUniversalTime();

            if (trip.EndDate.HasValue)
                trip.EndDate = trip.EndDate.Value.ToUniversalTime();

            await _tripDbContext.Trips.InsertOneAsync(trip);
        }
    }
}
