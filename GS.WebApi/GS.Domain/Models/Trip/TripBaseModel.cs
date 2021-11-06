using System;

namespace GS.Domain.Models.Trip
{
    public class TripBaseModel
    {
        public Guid UserId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }
    }
}
