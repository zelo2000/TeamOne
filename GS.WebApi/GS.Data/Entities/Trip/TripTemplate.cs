using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace GS.Data.Entities
{
    public class TripTemplate : BaseObject
    {
        public string Name { get; set; }

        [BsonElement]
        public List<ItemToTake> ItemsToTake { get; set; }

        [BsonElement]
        public List<ToDoNode> ToDoNodes { get; set; }
    }
}
