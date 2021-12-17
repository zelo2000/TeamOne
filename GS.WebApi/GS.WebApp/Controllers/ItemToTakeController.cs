using GS.Business.Command;
using GS.Business.Infrastructure.Command;
using GS.Business.Infrastructure.Query;
using GS.Business.Query;
using GS.Domain.Models.ItemToTake;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GS.WebApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ItemToTakeController : ControllerBase
    {
        private readonly ICommandHandler _commandHandler;
        private readonly IQueryHandler _queryHandler;

        public ItemToTakeController(ICommandHandler commandHandler, IQueryHandler queryHandler)
        {
            _commandHandler = commandHandler;
            _queryHandler = queryHandler;
        }

        [HttpGet("{tripId}")]
        public async Task<ActionResult<ItemToTakeModel>> GetById(Guid tripId)
        {
            var query = new GetItemsToTakeQuery(tripId);
            var nodes = await _queryHandler.Handle<GetItemsToTakeQuery, IEnumerable<ItemToTakeModel>>(query);
            return Ok(nodes);
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
