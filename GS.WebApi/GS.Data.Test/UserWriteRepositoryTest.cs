using AutoFixture;
using FluentAssertions;
using GS.Data.Entities;
using GS.Data.Repositories.UserWrite;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace GS.Data.Test
{
    public class UserWriteRepositoryTest : BaseRepositoryTest
    {
        private UserWriteRepository _userWriteRepository;
        private GSDbContext _dbContext;

        [SetUp]
        public void SetUp()
        {
            var options = new DbContextOptionsBuilder<GSDbContext>()
                .UseInMemoryDatabase(databaseName: "Test" + Guid.NewGuid())
                .Options;

            _dbContext = new GSDbContext(options);
            _userWriteRepository = new UserWriteRepository(_dbContext);
        }

        [Test]
        public async Task AddUser_ShoulAddUsers()
        {
            var user = _fixture.Create<User>();

            await _userWriteRepository.AddUserAsync(user);

            var userAfterAdd = await _dbContext.Users.ToListAsync();
            userAfterAdd.Should().HaveCount(1);
        }

        [Test]
        public async Task AddUser_ShoulAddUserLogins()
        {
            var userLogins = _fixture.Create<UserLogin>();

            await _userWriteRepository.AddUserLoginAsync(userLogins);

            var userAfterAdd = await _dbContext.UserLogins.ToListAsync();
            userAfterAdd.Should().HaveCount(1);
        }
    }
}
