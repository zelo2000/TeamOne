using Microsoft.AspNetCore.Mvc;

namespace GS.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        public UserController()
        {
        }

        [HttpGet]
        public IActionResult GetUser()
        {
            return Ok();
        }
    }
}
