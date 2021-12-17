using GS.Data.Entities;
using GS.Domain.Enums;
using GS.Domain.Models.Trip;
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

        public async Task UpdateTrip(Guid tripId, TripBaseModel trip)
        {
            var update = Builders<Trip>.Update.Set("Name", trip.Name)
                .Set("Description", trip.Description)
                .Set("EndDate", trip.EndDate)
                .Set("StartDate", trip.StartDate);

            await _tripDbContext.Trips.UpdateOneAsync(t => t.Id == tripId, update);
        }

        public async Task DeleteTrip(Guid tripId)
        {
            await _tripDbContext.Trips.DeleteOneAsync(t => t.Id == tripId);
        }

        public async Task SetTripStatus(Guid tripId, TripStatus status)
        {
            var update = Builders<Trip>.Update.Set("Status", status);
            await _tripDbContext.Trips.UpdateOneAsync(t => t.Id == tripId, update);
        }

        public async Task AddToDoNode(Guid tripId, ToDoNode node)
        {
            var update = Builders<Trip>.Update.AddToSet("ToDoNodes", node);
            await _tripDbContext.Trips.UpdateOneAsync(t => t.Id == tripId, update);
        }

        public async Task UpdateToDoNode(Guid nodeId, ToDoNode node)
        {
            var filter = Builders<Trip>.Filter.Eq("ToDoNodes._id", nodeId);
            var update = Builders<Trip>.Update.Set("ToDoNodes.$.Name", node.Name)
                .Set("ToDoNodes.$.Description", node.Description)
                .Set("ToDoNodes.$.Type", node.Type)
                .Set("ToDoNodes.$.Date", node.Date);

            await _tripDbContext.Trips.UpdateOneAsync(filter, update);
        }

        public async Task SetToDoNodeStatus(Guid nodeId, NodeStatus status)
        {
            var filter = Builders<Trip>.Filter.Eq("ToDoNodes._id", nodeId);
            var update = Builders<Trip>.Update.Set("ToDoNodes.$.Status", status);

            await _tripDbContext.Trips.UpdateOneAsync(filter, update);
        }

        public async Task DeleteToDoNode(Guid nodeId)
        {
            var filter = Builders<Trip>.Filter.Eq("ToDoNodes._id", nodeId);
            var update = Builders<Trip>.Update.PullFilter(t => t.ToDoNodes, Builders<ToDoNode>
                .Filter.Where(td => td.Id == nodeId));

            await _tripDbContext.Trips.UpdateOneAsync(filter, update);
        }

        public async Task AddItem(Guid tripId, ItemToTake item)
        {
            var update = Builders<Trip>.Update.AddToSet("ItemsToTake", item);
            await _tripDbContext.Trips.UpdateOneAsync(t => t.Id == tripId, update);
        }

        public async Task UpdateItem(Guid itemId, ItemToTake item)
        {
            var filter = Builders<Trip>.Filter.Eq("ItemsToTake._id", itemId);
            var update = Builders<Trip>.Update.Set("ItemsToTake.$.Name", item.Name);

            await _tripDbContext.Trips.UpdateOneAsync(filter, update);
        }

        public async Task SetIsItemTaken(Guid itemId, bool isTaken)
        {
            var filter = Builders<Trip>.Filter.Eq("ItemsToTake._id", itemId);
            var update = Builders<Trip>.Update.Set("ItemsToTake.$.IsTaken", isTaken);

            await _tripDbContext.Trips.UpdateOneAsync(filter, update);
        }

        public async Task DeleteItem(Guid itemId)
        {
            var filter = Builders<Trip>.Filter.Eq("ItemsToTake._id", itemId);
            var update = Builders<Trip>.Update.PullFilter(t => t.ItemsToTake, Builders<ItemToTake>
                .Filter.Where(it => it.Id == itemId));

            await _tripDbContext.Trips.UpdateOneAsync(filter, update);
        }
    }
}
