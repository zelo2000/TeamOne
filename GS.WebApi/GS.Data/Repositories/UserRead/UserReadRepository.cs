using GS.Data.Entities;
using Microsoft.EntityFrameworkCore;
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

        public async Task<User> GetUserByLoginAsync(string loginProvider, string providerKey)
        {
            var userLogin = await _dbContext.UserLogins
                .Include(ul => ul.User)
                .FirstOrDefaultAsync(ul => ul.LoginProvider == loginProvider && ul.ProviderKey == providerKey);

            return userLogin?.User;
        }
    }
}
