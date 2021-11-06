using GS.Domain.Enums;
using System;

namespace GS.Domain.Models.ToDoNode
{
    public class ToDoNodeModel : ToDoNodeBaseModel
    {
        public Guid Id { get; set; }

        public NodeStatus Status { get; set; }
    }
}
