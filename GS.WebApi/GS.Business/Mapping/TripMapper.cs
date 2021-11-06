using GS.Data.Entities;
using GS.Domain.Models.Trip;
using System.Linq;

namespace GS.Business.Mapping
{
    public static class TripMapper
    {
        public static Trip ToEntity(this TripBaseModel model)
        {
            return new Trip
            {
                UserId = model.UserId,
                Name = model.Name,
                Description = model.Description,
                StartDate = model.StartDate,
                EndDate = model.EndDate,
            };
        }

        public static TripModel ToDomain(this Trip trip)
        {
            return new TripModel
            {
                Id = trip.Id,
                UserId = trip.UserId,
                Name = trip.Name,
                Description = trip.Description,
                StartDate = trip.StartDate,
                EndDate = trip.EndDate,
                Status = trip.Status,

                ToDoNodes = trip.ToDoNodes.Select(tdn => tdn.ToDomain()).ToList(),
                ItemsToTake = trip.ItemsToTake.Select(itt => itt.ToDomain()).ToList()
            };
        }
    }
}
