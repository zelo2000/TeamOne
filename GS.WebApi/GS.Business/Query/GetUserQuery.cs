using GS.Business.Infrastructure.Query;
using GS.Business.Mapping;
using GS.Data.Repositories.UserRead;
using GS.Domain.Models.User;
using System;
using System.Threading.Tasks;

namespace GS.Business.Query
{
    public class GetUserQuery : IQuery
    {
        public Guid UserId { get; }

        public GetUserQuery(Guid userId)
        {
            UserId = userId;
        }
    }

    public class GetUserHandler : IQueryHandler<GetUserQuery, UserModel>
    {
        private readonly IUserReadRepository _userReadRepository;

        public GetUserHandler(IUserReadRepository userReadRepository)
        {
            _userReadRepository = userReadRepository;
        }

        public async Task<UserModel> Handle(GetUserQuery query)
        {
            var userEntity = await _userReadRepository.GetUserById(query.UserId);
            return userEntity.ToDomain();
        }
    }
}
