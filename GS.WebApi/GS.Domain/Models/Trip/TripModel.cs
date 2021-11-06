using GS.Domain.Enums;
using GS.Domain.Models.ItemToTake;
using GS.Domain.Models.ToDoNode;
using System;
using System.Collections.Generic;

namespace GS.Domain.Models.Trip
{
    public class TripModel : TripBaseModel
    {
        public Guid Id { get; set; }

        public TripStatus Status { get; set; }

        public IEnumerable<ItemToTakeModel> ItemsToTake { get; set; }

        public IEnumerable<ToDoNodeModel> ToDoNodes { get; set; }
    }
}
