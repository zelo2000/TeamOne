using GS.Business.Infrastructure.Query;
using GS.Business.Mapping;
using GS.Data.Repositories.TripRead;
using GS.Domain.Models.ToDoNode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GS.Business.Query
{
    public class GetToDoNodesQuery : IQuery
    {
        public GetToDoNodesQuery(Guid tripId)
        {
            TripId = tripId;
        }

        public Guid TripId { get; set; }
    }

    public class GetToDoNodesQueryHandler : IQueryHandler<GetToDoNodesQuery, IEnumerable<ToDoNodeModel>>
    {
        private readonly ITripReadRepository _tripReadRepository;

        public GetToDoNodesQueryHandler(ITripReadRepository tripReadRepository)
        {
            _tripReadRepository = tripReadRepository;
        }

        public async Task<IEnumerable<ToDoNodeModel>> Handle(GetToDoNodesQuery query)
        {
            var nodes = await _tripReadRepository.GetToDoNodes(query.TripId);
            var result = nodes.Select(t => t.ToDomain());
            return result;
        }
    }
}
