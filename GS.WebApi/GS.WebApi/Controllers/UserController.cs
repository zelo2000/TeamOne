using GS.Business.Infrastructure.Query;
using GS.Business.Query;
using GS.Domain.Models;
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

        public UserController(IQueryHandler queryHandler)
        {
            _queryHandler = queryHandler;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(Guid id)
        {
            var query = new GetUserQuery(id);
            var user = await _queryHandler.Handle<GetUserQuery, User>(query);
            return Ok(user);
        }
    }
}
