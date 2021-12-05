using GS.Business.Command;
using GS.Business.Infrastructure.Command;
using GS.Domain.Models.ItemToTake;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace GS.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ItemToTakeController : ControllerBase
    {
        private readonly ICommandHandler _commandHandler;

        public ItemToTakeController(ICommandHandler commandHandler)
        {
            _commandHandler = commandHandler;
        }

        [HttpPost("{tripId}")]
        public async Task<IActionResult> Add(Guid tripId, [FromBody] ItemToTakeBaseModel item)
        {
            var command = new AddItemCommand(tripId, item);
            await _commandHandler.Handle(command);
            return Ok();
        }

        [HttpPut("{itemId}")]
        public async Task<IActionResult> Update(Guid itemId, [FromBody] ItemToTakeBaseModel item)
        {
            var command = new UpdateItemCommand(itemId, item);
            await _commandHandler.Handle(command);
            return Ok();
        }

        [HttpPatch("{itemId}/{isTaken}")]
        public async Task<IActionResult> SetIsTaken(Guid itemId, bool isTaken)
        {
            var command = new SetIsItemTakenCommand(itemId, isTaken);
            await _commandHandler.Handle(command);
            return Ok();
        }

        [HttpDelete("{itemId}")]
        public async Task<IActionResult> Delete(Guid itemId)
        {
            var command = new DeleteItemCommand(itemId);
            await _commandHandler.Handle(command);
            return Ok();
        }
    }
}
