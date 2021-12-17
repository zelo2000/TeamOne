using GS.Business.Infrastructure;
using GS.Domain.Models.User;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace GS.WebApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _autService;

        public AuthController(IAuthService autService)
        {
            _autService = autService;
        }

        [HttpPost("ExternalLogin")]
        public async Task<IActionResult> ExternalLogin([FromBody] ExternalAuthDto externalAuth)
        {
            var payload = await _autService.VerifyGoogleToken(externalAuth);
            if (payload == null)
            {
                return BadRequest("Invalid External Authentication.");
            }

            var user = await _autService.GetUserByLoginAsync("GOOGLE", payload.Subject);
            if (user == null)
            {
                user = new UserModel
                {
                    Id = Guid.NewGuid(),
                    Email = payload.Email,
                    Username = payload.Name
                };

                await _autService.AddUserAsync(user);

                var newLoginModel = new UserLoginModel
                {
                    LoginProvider = "GOOGLE",
                    ProviderDisplayName = "Google",
                    ProviderKey = payload.Subject,
                    UserId = user.Id,
                };

                await _autService.AddUserLoginAsync(newLoginModel);
            }

            if (user == null)
            {
                return BadRequest("Invalid External Authentication.");
            }

            var token = _autService.GenerateToken(user);

            var responce = new AuthResponseDto
            {
                Token = token,
                Email = user.Email,
                Id = user.Id
            };

            return Ok(responce);
        }
    }
}
