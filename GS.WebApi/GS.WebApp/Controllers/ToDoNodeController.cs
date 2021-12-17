using GS.Business.Command;
using GS.Business.Infrastructure.Command;
using GS.Business.Infrastructure.Query;
using GS.Business.Query;
using GS.Domain.Enums;
using GS.Domain.Models.ToDoNode;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GS.WebApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ToDoNodeController : ControllerBase
    {
        private readonly ICommandHandler _commandHandler;
        private readonly IQueryHandler _queryHandler;

        public ToDoNodeController(ICommandHandler commandHandler, IQueryHandler queryHandler)
        {
            _commandHandler = commandHandler;
            _queryHandler = queryHandler;
    }

        [HttpGet("{tripId}")]
        public async Task<ActionResult<ToDoNodeModel>> GetById(Guid tripId)
        {
            var query = new GetToDoNodesQuery(tripId);
            var nodes = await _queryHandler.Handle<GetToDoNodesQuery, IEnumerable<ToDoNodeModel>>(query);
            return Ok(nodes);
        }

        [HttpPost("{tripId}")]
        public async Task<IActionResult> Add(Guid tripId, [FromBody] ToDoNodeBaseModel node)
        {
            var command = new AddToDoNodeCommand(tripId, node);
            await _commandHandler.Handle(command);
            return Ok();
        }

        [HttpPut("{nodeId}")]
        public async Task<IActionResult> Update(Guid nodeId, [FromBody] ToDoNodeBaseModel node)
        {
            var command = new UpdateToDoNodeCommand(nodeId, node);
            await _commandHandler.Handle(command);
            return Ok();
        }

        [HttpPatch("{nodeId}/{status}")]
        public async Task<IActionResult> SetStatus(Guid nodeId, NodeStatus status)
        {
            var command = new SetToDoNodeStatusCommand(nodeId, status);
            await _commandHandler.Handle(command);
            return Ok();
        }

        [HttpDelete("{nodeId}")]
        public async Task<IActionResult> Delete(Guid nodeId)
        {
            var command = new DeleteToDoNodeCommand(nodeId);
            await _commandHandler.Handle(command);
            return Ok();
        }
    }
}
