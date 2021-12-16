using GS.Data.Entities;
using System.Threading.Tasks;

namespace GS.Data.Repositories.UserWrite
{

    public class UserWriteRepository : IUserWriteRepository
    {
        private readonly GSDbContext _dbContext;

        public UserWriteRepository(GSDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddUserAsync(User user)
        {
            await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();
        }

        public async Task AddUserLoginAsync(UserLogin userLogin)
        {
            await _dbContext.UserLogins.AddAsync(userLogin);
            await _dbContext.SaveChangesAsync();
        }
    }
}
