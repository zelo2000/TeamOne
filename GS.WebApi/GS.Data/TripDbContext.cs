using GS.Data.Entities;
using GS.Domain.Models.Configuration;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace GS.Data
{
    public class TripDbContext
    {
        public IMongoCollection<Trip> Trips { get; set; }

        public IMongoCollection<Trip> TripTemplates { get; set; }

        public TripDbContext(IOptions<MongoDbSettings> option)
        {
            var settings = option.Value;

            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            Trips = database.GetCollection<Trip>("trips");
            TripTemplates = database.GetCollection<Trip>("trip-templates");
        }
    }
}
