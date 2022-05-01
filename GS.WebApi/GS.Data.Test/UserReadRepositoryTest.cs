using AutoFixture;
using FluentAssertions;
using GS.Data.Entities;
using GS.Data.Repositories.UserRead;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace GS.Data.Test
{
    public class UserReadRepositoryTest: BaseRepositoryTest
    {
        private UserReadRepository _userReadRepository;
        private GSDbContext _dbContext;


        [SetUp]
        public void SetUp()
        {
            var options = new DbContextOptionsBuilder<GSDbContext>()
                .UseInMemoryDatabase(databaseName: "Test" + Guid.NewGuid())
                .Options;

            _dbContext = new GSDbContext(options);
            _userReadRepository = new UserReadRepository(_dbContext);
        }

        [Test]
        public async Task GetUserByLoginAsync_WithExistingData_User()
        {
            var user = _fixture.Create<User>();
            _dbContext.Add(user);
            _dbContext.SaveChanges();

            var userLogin = user.UserLogins.FirstOrDefault();

            var userFromDb = await _userReadRepository.GetUserByLoginAsync(userLogin.LoginProvider, userLogin.ProviderKey);

            userFromDb.Should().NotBeNull();
            userFromDb.Id.Should().Be(user.Id);
        }

        [Test]
        public async Task GetUserByLoginAsync_WithoutExistingLoginProvide_User()
        {
            var user = _fixture.Create<User>();
            _dbContext.Add(user);
            _dbContext.SaveChanges();

            var userLogin = user.UserLogins.FirstOrDefault();

            var userFromDb = await _userReadRepository.GetUserByLoginAsync(Guid.NewGuid().ToString(), userLogin.ProviderKey);

            userFromDb.Should().BeNull();
        }

        [Test]
        public async Task GetUserByLoginAsync_WithoutExistingProviderKey_User()
        {
            var user = _fixture.Create<User>();
            _dbContext.Add(user);
            _dbContext.SaveChanges();

            var userLogin = user.UserLogins.FirstOrDefault();

            var userFromDb = await _userReadRepository.GetUserByLoginAsync(userLogin.LoginProvider, Guid.NewGuid().ToString());

            userFromDb.Should().BeNull();
        }

        [Test]
        public async Task GetUserByLoginAsync_WithoutExistingData_User()
        {
            var user = _fixture.Create<User>();
            _dbContext.Add(user);
            _dbContext.SaveChanges();

            var userFromDb = await _userReadRepository.GetUserByLoginAsync(Guid.NewGuid().ToString(), Guid.NewGuid().ToString());

            userFromDb.Should().BeNull();
        }
    }
}
