using GS.Business.Infrastructure.Query;
using GS.Business.Mapping;
using GS.Data.Repositories.TripRead;
using GS.Domain.Models.Trip;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GS.Business.Query
{
    public class GetUserTripsQuery : IQuery
    {
        public GetUserTripsQuery(Guid userId)
        {
            UserId = userId;
        }

        public Guid UserId { get; set; }
    }

    public class GetTripsUserQueryHandler : IQueryHandler<GetUserTripsQuery, IEnumerable<TripModel>>
    {
        private readonly ITripReadRepository _tripReadRepository;

        public GetTripsUserQueryHandler(ITripReadRepository tripReadRepository)
        {
            _tripReadRepository = tripReadRepository;
        }

        public async Task<IEnumerable<TripModel>> Handle(GetUserTripsQuery query)
        {
            var trips = await _tripReadRepository.GetUserTrips(query.UserId);
            var result = trips.Select(t => t.ToDomain());
            return result;
        }
    }
}
