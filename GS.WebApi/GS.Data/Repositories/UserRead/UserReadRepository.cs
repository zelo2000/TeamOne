using GS.Data.Entities;
using System;

namespace GS.Data.Repositories.UserRead
{
    public class UserReadRepository : IUserReadRepository
    {
        private readonly GSDbContext _dbContext;

        public UserReadRepository(GSDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public User GetUserById(Guid userId)
        {
            return new User
            {
                Id = Guid.Empty,
                Email = "test@test.com",
                Username = "test_user"
            };
        }
    }
}
