using GS.Data.Entities;
using GS.Data.Repositories.TripRead;
using GS.Data.Repositories.TripWrite;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GS.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TestController : ControllerBase
    {
        private readonly ITripReadRepository _tripReadRepository;
        private readonly ITripWriteRepository _tripWriteRepository;

        public TestController(ITripReadRepository tripReadRepository, ITripWriteRepository tripWriteRepository)
        {
            _tripReadRepository = tripReadRepository;
            _tripWriteRepository = tripWriteRepository;
        }

        [HttpGet("userId")]
        public async Task<ActionResult<List<Trip>>> GetTripForUser(Guid userId)
        {
            var trips = await _tripReadRepository.GetTripForUser(userId);
            return Ok(trips);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTrip([FromBody] Trip trip)
        {
            await _tripWriteRepository.CreateTrip(trip);
            return Ok();
        }
    }
}
