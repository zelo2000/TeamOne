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
    public class GetTripQuery : IQuery
    {
        public GetTripQuery(Guid tripId)
        {
            TripId = tripId;
        }

        public Guid TripId { get; set; }
    }

    public class GetTripQueryHandler : IQueryHandler<GetTripQuery, IEnumerable<TripModel>>
    {
        private readonly ITripReadRepository _tripReadRepository;

        public GetTripQueryHandler(ITripReadRepository tripReadRepository)
        {
            _tripReadRepository = tripReadRepository;
        }

        public async Task<IEnumerable<TripModel>> Handle(GetTripQuery query)
        {
            var trips = await _tripReadRepository.GetTripById(query.TripId);
            var result = trips.Select(t => t.ToDomain());
            return result;
        }
    }
}
