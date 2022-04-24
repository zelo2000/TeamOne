using FluentAssertions;
using GS.Data.Entities;
using GS.Data.Repositories.UserRead;
using GS.Data.Repositories.UserWrite;
using GS.Domain.Models.Configuration;
using GS.Domain.Models.User;
using Microsoft.Extensions.Options;
using Moq;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace GS.Business.Test
{
    public class AuthServiceTest
    {
        private Mock<IUserWriteRepository> _userWriteRepository;
        private Mock<IUserReadRepository> _userReadRepository;
        private AuthService _authService;

        [SetUp]
        public void SetUp()
        {
            _userReadRepository = new Mock<IUserReadRepository>();
            _userWriteRepository = new Mock<IUserWriteRepository>();

            var jwtSettings = new JwtSettings
            {
                ExpiryInMinutes = 5,
                ValidAudience = "test",
                SecurityKey = "testtesttesttesttesttesttest",
                ValidIssuer = "test"
            };
            var jwtOptions = Options.Create(jwtSettings);

            var googleAuthSettings = new GoogleAuthSettings
            {
                ClientId = "test"
            };
            var googleOption = Options.Create(googleAuthSettings);

            _authService = new AuthService(jwtOptions, googleOption, _userReadRepository.Object, _userWriteRepository.Object);
        }

        [Test]
        public void GenerateToken_WithUser_Token()
        {
            var user = new UserModel
            {
                Email = "test@test.com"
            };

            var result = _authService.GenerateToken(user);

            result.Should().NotBeNullOrWhiteSpace();
        }

        [Test]
        public async Task GetUserByLoginAsync_WithLogin_User()
        {
            var user = new User()
            {
                Id = Guid.Empty
            };

            _userReadRepository.Setup(x => x.GetUserByLoginAsync(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(user);

            var result = await _authService.GetUserByLoginAsync("test", "test");

            result.Should().NotBeNull();
            result.Id.Should().Be(Guid.Empty);
        }

        [Test]
        public void AddUserAsync_WithUser_ShouldAdd()
        {
            var model = new UserModel()
            {
                Id = Guid.Empty
            };

            _userWriteRepository.Setup(x => x.AddUserAsync(It.IsAny<User>()))
                .Returns(Task.CompletedTask);

            Assert.DoesNotThrowAsync(async () => await _authService.AddUserAsync(model));
        }

        [Test]
        public void AddUserLoginAsync_WithUser_ShouldAdd()
        {
            var model = new UserLoginModel()
            {
                UserId = Guid.Empty
            };

            _userWriteRepository.Setup(x => x.AddUserLoginAsync(It.IsAny<UserLogin>()))
                .Returns(Task.CompletedTask);

            Assert.DoesNotThrowAsync(async () => await _authService.AddUserLoginAsync(model));
        }
    }
}
