using GS.Business.Command;
using GS.Business.Infrastructure.Command;
using GS.Business.Infrastructure.Query;
using GS.Business.Query;
using GS.Domain.Models.User;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace GS.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IQueryHandler _queryHandler;
        private readonly ICommandHandler _commandHandler;

        public UserController(IQueryHandler queryHandler, ICommandHandler commandHandler)
        {
            _queryHandler = queryHandler;
            _commandHandler = commandHandler;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserModel>> GetById(Guid id)
        {
            var query = new GetUserQuery(id);
            var user = await _queryHandler.Handle<GetUserQuery, UserModel>(query);
            return Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterModel user)
        {
            var command = new RegisterUserCommand(user);
            await _commandHandler.Handle(command);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] RegisterModel user)
        {
            throw new NotImplementedException();
        }
    }
}
