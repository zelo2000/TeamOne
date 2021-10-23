using GS.Data.Entities;
using MongoDB.Driver;
using System;
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
            await _tripDbContext.Trips.InsertOneAsync(trip);
        }

        public async Task AddToDoNode(Guid tripId, ToDoNode node)
        {
            node.Id = Guid.NewGuid();
            var update = Builders<Trip>.Update.AddToSet("ToDoNodes", node);
            await _tripDbContext.Trips.UpdateOneAsync(t => t.Id == tripId, update);
        }
    }
}
