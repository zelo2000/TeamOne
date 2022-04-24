using GS.Data.Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GS.Data.Repositories.TripRead
{
    public class TripReadRepository : ITripReadRepository
    {
        private readonly TripDbContext _tripDbContext;

        public TripReadRepository(TripDbContext tripDbContext)
        {
            _tripDbContext = tripDbContext;
        }

        public List<Trip> GetTripList()
        {
            var trips = _tripDbContext.Trips.AsQueryable();
            return trips.ToList();
        }

        public async Task<IEnumerable<Trip>> GetUserTrips(Guid userId)
        {
            var trips = await _tripDbContext.Trips.FindAsync(x => x.UserId == userId);
            return trips.ToList();
        }

        public async Task<Trip> GetTripById(Guid tripId)
        {
            var trips = await _tripDbContext.Trips.FindAsync(x => x.Id == tripId);
            return trips.FirstOrDefault();
        }

        public async Task<IEnumerable<ToDoNode>> GetToDoNodes(Guid tripId)
        {
            var trips = await _tripDbContext.Trips.FindAsync(x => x.Id == tripId);
            var trip = await trips.FirstOrDefaultAsync();
            
            return trip.ToDoNodes.ToList();
        }

        public async Task<IEnumerable<ItemToTake>> GetItemsToTake(Guid tripId)
        {
            var trips = await _tripDbContext.Trips.FindAsync(x => x.Id == tripId);
            var trip = await trips.FirstOrDefaultAsync();

            return trip.ItemsToTake.ToList();
        }
    }
}
