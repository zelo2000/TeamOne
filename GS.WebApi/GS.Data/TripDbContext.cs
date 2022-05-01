using GS.Data.Entities;
using GS.Domain.Models.Configuration;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace GS.Data
{
    public class TripDbContext
    {
        public IMongoDatabase _mongoDatabase;

        public TripDbContext(IMongoClient mongoClient, IOptions<MongoDbSettings> options)
        {
            _mongoDatabase = mongoClient.GetDatabase(options.Value.DatabaseName);
            Trips = _mongoDatabase.GetCollection<Trip>("trips");
        }

        public virtual IMongoCollection<Trip> Trips { get; set; }
    }
}
