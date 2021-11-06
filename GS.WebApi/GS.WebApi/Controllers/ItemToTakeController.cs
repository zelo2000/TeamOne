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
            throw new NotImplementedException();
        }

        [HttpPut("{itemId}")]
        public async Task<IActionResult> Udpate(Guid itemId, [FromBody] ItemToTakeBaseModel item)
        {
            throw new NotImplementedException();
        }

        [HttpPatch("{itemId}/{isTaken}")]
        public async Task<IActionResult> SetIsTaken(Guid itemId, bool isTaken)
        {
            throw new NotImplementedException();
        }

        [HttpDelete("{itemId}")]
        public async Task<IActionResult> Delete(Guid itemId)
        {
            throw new NotImplementedException();
        }
    }
}
