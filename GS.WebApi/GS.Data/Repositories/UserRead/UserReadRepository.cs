namespace GS.Data.Repositories.UserRead
{
    public interface IUserReadRepository
    {

    }

    public class UserReadRepository : IUserReadRepository
    {
        private readonly GSDbContext _dbContext;

        public UserReadRepository(GSDbContext dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
