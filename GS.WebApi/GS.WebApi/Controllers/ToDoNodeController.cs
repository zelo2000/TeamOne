using GS.Business.Command;
using GS.Business.Infrastructure.Command;
using GS.Domain.Enums;
using GS.Domain.Models.ToDoNode;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace GS.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ToDoNodeController : ControllerBase
    {
        private readonly ICommandHandler _commandHandler;

        public ToDoNodeController(ICommandHandler commandHandler)
        {
            _commandHandler = commandHandler;
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
