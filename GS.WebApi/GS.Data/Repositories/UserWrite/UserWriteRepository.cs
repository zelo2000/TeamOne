﻿namespace GS.Data.Repositories.UserWrite
{

    public class UserWriteRepository : IUserWriteRepository
    {
        private readonly GSDbContext _dbContext;

        public UserWriteRepository(GSDbContext dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
