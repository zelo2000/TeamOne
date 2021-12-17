using GS.Business.Command;
using GS.Business.Infrastructure.Command;
using GS.Business.Infrastructure.Query;
using GS.Business.Query;
using GS.Domain.Models.Trip;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GS.WebApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TripController : ControllerBase
    {
        private readonly ICommandHandler _commandHandler;
        private readonly IQueryHandler _queryHandler;

        public TripController(ICommandHandler commandHandler, IQueryHandler queryHandler)
        {
            _commandHandler = commandHandler;
            _queryHandler = queryHandler;
        }

        [HttpGet("{tripId}")]
        public async Task<ActionResult<TripModel>> GetById(Guid tripId)
        {
            var query = new GetTripQuery(tripId);
            var trips = await _queryHandler.Handle<GetTripQuery, IEnumerable<TripModel>>(query);
            return Ok(trips);
        }

        [HttpGet("user/{userId}")]
        public async Task<ActionResult<IEnumerable<TripModel>>> GetByUserId(Guid userId)
        {
            var query = new GetUserTripsQuery(userId);
            var trips = await _queryHandler.Handle<GetUserTripsQuery, IEnumerable<TripModel>>(query);
            return Ok(trips);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TripBaseModel trip)
        {
            var command = new CreateTripCommand(trip);
            await _commandHandler.Handle(command);
            return Ok();
        }

        [HttpPut("{tripId}")]
        public async Task<IActionResult> Update(Guid tripId, [FromBody] TripBaseModel trip)
        {
            var command = new UpdateTripCommand(tripId, trip);
            await _commandHandler.Handle(command);
            return Ok();
        }

        [HttpDelete("{tripId}")]
        public async Task<IActionResult> Delete(Guid tripId)
        {
            var command = new DeleteTripCommand(tripId);
            await _commandHandler.Handle(command);
            return Ok();
        }
    }
}
