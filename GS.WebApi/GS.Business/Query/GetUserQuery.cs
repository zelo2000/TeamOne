using GS.Business.Infrastructure.Query;
using GS.Business.Mapping;
using GS.Data.Repositories.UserRead;
using GS.Domain.Models;
using System;
using System.Threading.Tasks;

namespace GS.Business.Query
{
    public class GetUserQuery : IQuery<User>
    {
        public Guid UserId { get; }

        public GetUserQuery(Guid userId)
        {
            UserId = userId;
        }
    }

    public class GetUserHandler : IQueryHandler<GetUserQuery, User>
    {
        private readonly IUserReadRepository _userReadRepository;

        public GetUserHandler(IUserReadRepository userReadRepository)
        {
            _userReadRepository = userReadRepository;
        }

        public async Task<User> Handle(GetUserQuery query)
        {
            var userEntity = _userReadRepository.GetUserById(query.UserId);
            return userEntity.ToDomain();
        }
    }
}
