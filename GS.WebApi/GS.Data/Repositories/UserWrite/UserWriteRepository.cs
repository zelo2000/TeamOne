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

        public async Task AddUser(User user)
        {
            await _dbContext.Users.AddAsync(user);
            _dbContext.SaveChanges();
        }
    }
}
