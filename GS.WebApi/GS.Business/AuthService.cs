using Google.Apis.Auth;
using GS.Business.Infrastructure;
using GS.Business.Mapping;
using GS.Data.Repositories.UserRead;
using GS.Data.Repositories.UserWrite;
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
        private readonly JwtSettings _jwtSettings;
        private readonly GoogleAuthSettings _goolgeSettings;
        private readonly IUserWriteRepository _userWriteRepository;
        private readonly IUserReadRepository _userReadRepository;

        public AuthService(IOptions<JwtSettings> jwtOption,
            IOptions<GoogleAuthSettings> googleOption,
            IUserReadRepository userReadRepository,
            IUserWriteRepository userWriteRepository)
        {
            _jwtSettings = jwtOption.Value;
            _goolgeSettings = googleOption.Value;
            _userWriteRepository = userWriteRepository;
            _userReadRepository = userReadRepository;
        }

        public string GenerateToken(UserModel user)
        {
            var signingCredentials = GetSigningCredentials();
            var claims = GetClaims(user);
            var tokenOptions = GenerateTokenOptions(signingCredentials, claims);
            var token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

            return token;
        }

        public async Task<GoogleJsonWebSignature.Payload> VerifyGoogleToken(ExternalAuthDto externalAuth)
        {
            try
            {
                var settings = new GoogleJsonWebSignature.ValidationSettings()
                {
                    Audience = new List<string>() { _goolgeSettings.ClientId }
                };

                var payload = await GoogleJsonWebSignature.ValidateAsync(externalAuth.IdToken, settings);
                return payload;
            }
            catch
            {
                return null;
            }
        }

        public async Task<UserModel> GetUserByLoginAsync(string loginProvider, string providerKey)
        {
            var user = await _userReadRepository.GetUserByLoginAsync(loginProvider, providerKey);
            return user?.ToDomain();
        }

        public async Task AddUserAsync(UserModel user)
        {
            var userEntity = user.ToEntity();
            await _userWriteRepository.AddUserAsync(userEntity);
        }

        public async Task AddUserLoginAsync(UserLoginModel userLogin)
        {
            var userLoginEntity = userLogin.ToEntity();
            await _userWriteRepository.AddUserLoginAsync(userLoginEntity);
        }

        private SigningCredentials GetSigningCredentials()
        {
            var key = Encoding.UTF8.GetBytes(_jwtSettings.SecurityKey);
            var secret = new SymmetricSecurityKey(key);

            return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
        }

        private List<Claim> GetClaims(UserModel user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Email)
            };

            return claims;
        }

        private JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, List<Claim> claims)
        {
            var tokenOptions = new JwtSecurityToken(
                issuer: _jwtSettings.ValidIssuer,
                audience: _jwtSettings.ValidAudience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(_jwtSettings.ExpiryInMinutes),
                signingCredentials: signingCredentials);

            return tokenOptions;
        }
    }
}
