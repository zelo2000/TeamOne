using GS.Domain.Enums;
using System;

namespace GS.Domain.Models.ToDoNode
{
    public class ToDoNodeBaseModel
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public NodeType Type { get; set; }

        public DateTime? Date { get; set; }
    }
}
