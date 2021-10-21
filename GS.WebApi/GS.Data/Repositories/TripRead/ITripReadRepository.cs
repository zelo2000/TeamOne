using GS.Data.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GS.Data.Repositories.TripRead
{
    public interface ITripReadRepository
    {
        Task<IEnumerable<Trip>> GetTripForUser(Guid userId);
    }
}
