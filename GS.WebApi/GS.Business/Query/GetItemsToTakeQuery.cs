using GS.Business.Infrastructure.Query;
using GS.Business.Mapping;
using GS.Data.Repositories.TripRead;
using GS.Domain.Models.ItemToTake;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GS.Business.Query
{
    public class GetItemsToTakeQuery : IQuery
    {
        public GetItemsToTakeQuery(Guid tripId)
        {
            TripId = tripId;
        }

        public Guid TripId { get; set; }
    }

    public class GetItemsToTakeQueryHandler : IQueryHandler<GetItemsToTakeQuery, IEnumerable<ItemToTakeModel>>
    {
        private readonly ITripReadRepository _tripReadRepository;

        public GetItemsToTakeQueryHandler(ITripReadRepository tripReadRepository)
        {
            _tripReadRepository = tripReadRepository;
        }

        public async Task<IEnumerable<ItemToTakeModel>> Handle(GetItemsToTakeQuery query)
        {
            var nodes = await _tripReadRepository.GetItemsToTake(query.TripId);
            var result = nodes.Select(t => t.ToDomain());
            return result;
        }
    }
}
