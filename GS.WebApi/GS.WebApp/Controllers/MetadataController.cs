using GS.Domain.Enums;
using GS.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace GS.WebApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MetadataController : ControllerBase
    {
        public MetadataController()
        {
        }

        [HttpGet("node/statuses")]
        public ActionResult<List<MetadataItem<NodeStatus>>> GetNodeStatuses()
        {
            throw new NotImplementedException();
        }

        [HttpGet("node/types")]
        public ActionResult<List<MetadataItem<NodeType>>> GetNodeTypes()
        {
            throw new NotImplementedException();
        }

        [HttpGet("trip/statuses")]
        public ActionResult<List<MetadataItem<TripStatus>>> GetTripStatuses()
        {
            throw new NotImplementedException();
        }
    }
}
