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
        public async Task<IActionResult> Udpate(Guid nodeId, [FromBody] ToDoNodeBaseModel node)
        {
            throw new NotImplementedException();
        }

        [HttpPatch("{nodeId}/{status}")]
        public async Task<IActionResult> SetStatus(Guid nodeId, NodeStatus status)
        {
            throw new NotImplementedException();
        }

        [HttpDelete("{nodeId}")]
        public async Task<IActionResult> Delete(Guid tripId, Guid nodeId)
        {
            throw new NotImplementedException();
        }
    }
}
