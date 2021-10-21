using GS.Domain.Enums;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace GS.Data.Entities
{
    public class ToDoNode : BaseObject
    {
        public string Name { get; set; }

        public string Description { get; set; }

        [BsonRepresentation(BsonType.Int32)]
        public NodeStatus Status { get; set; }
    }
}
