using GS.Data.Entities;
using System;
using System.Collections.Generic;
using MongoDB.Driver;
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

        public async Task<IEnumerable<Trip>> GetTripForUser(Guid userId)
        {
            var trips = await _tripDbContext.Trips.FindAsync(x => x.UserId == userId);
            return trips.ToList();
        }
    }
}
