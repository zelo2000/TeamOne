using GS.Business.Infrastructure;
using GS.Domain.Models;

namespace GS.Business.Query
{
    public class GetUserQuery : IQuery<User>
    {
    }

    public class GetUserHandler : IQueryHandler<GetUserQuery, User>
    {
        public User Handle(GetUserQuery query)
        {
            throw new System.NotImplementedException();
        }
    }
}
