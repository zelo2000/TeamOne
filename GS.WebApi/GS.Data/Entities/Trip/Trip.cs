using GS.Domain.Enums;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace GS.Data.Entities
{
    public class Trip : BaseObject
    {
        public Trip()
        {
            ItemsToTake = new List<ItemToTake>();
            ToDoNodes = new List<ToDoNode>();
        }

        [BsonRepresentation(BsonType.Int32)]
        public TripStatus Status { get; set; }

        public Guid UserId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }


        [BsonElement]
        public List<ItemToTake> ItemsToTake { get; set; }

        [BsonElement]
        public List<ToDoNode> ToDoNodes { get; set; }
    }
}
