using GS.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace GS.Data.Repositories.UserRead
{
    public class UserReadRepository : IUserReadRepository
    {
        private readonly GSDbContext _dbContext;

        public UserReadRepository(GSDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<User> GetByEmailAndPasswordHashAsync(string email, string passwordHash)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == email && u.PasswordHash == passwordHash);
            return user;
        }

        public async Task<User> GetUserById(Guid userId)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == userId);
            return user;
        }
    }
}
