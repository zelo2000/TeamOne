using GS.Business.Infrastructure;
using GS.Domain.Models.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace GS.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("log-in")]
        public async Task<ActionResult<LogInResultModel>> LogIn([FromBody] LogInModel model)
        {
            var result = await _authService.LogIn(model);
            return result;
        }

        [HttpPost("log-out")]
        [Authorize]
        public async Task<IActionResult> LogOut()
        {
            throw new NotImplementedException();
        }
    }
}
