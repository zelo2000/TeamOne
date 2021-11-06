using GS.Business.Infrastructure;
using GS.Data.Repositories.UserRead;
using GS.Domain.Contants;
using GS.Domain.Models.Configuration;
using GS.Domain.Models.User;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace GS.Business
{
    public class AuthService : IAuthService
    {
        private readonly IPasswordHashProvider _passwordHashProvider;
        private readonly IUserReadRepository _userReadRepository;
        private readonly AuthSetting _authSetting;

        public AuthService(IPasswordHashProvider passwordHashProvider, IUserReadRepository userReadRepository, IOptions<AuthSetting> options)
        {
            _passwordHashProvider = passwordHashProvider;
            _userReadRepository = userReadRepository;
            _authSetting = options.Value;
        }

        public async Task<LogInResultModel> LogIn(LogInModel model)
        {
            var user = await _userReadRepository.GetByEmailAndPasswordHashAsync(model.Email, _passwordHashProvider.GetPasswordHash(model.Password));

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_authSetting.SecretKey);

            var claims = new List<Claim>
            {
                new Claim(ClaimNames.Id, user.Id.ToString()),
            };

            var expDate = DateTime.UtcNow.AddHours(_authSetting.ExpiredAt);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = expDate,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
            };

            return new LogInResultModel
            {
                AccessToken = tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDescriptor)),
                UserId = user.Id
            };
        }
    }
}
