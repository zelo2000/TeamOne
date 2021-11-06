using GS.Data.Entities;
using GS.Domain.Models.ToDoNode;

namespace GS.Business.Mapping
{
    public static class ToDoNodeMapper
    {
        public static ToDoNode ToEntity(this ToDoNodeBaseModel model)
        {
            return new ToDoNode
            {
                Name = model.Name,
                Description = model.Description,
                Date = model.Date,
                Type = model.Type,
            };
        }

        public static ToDoNodeModel ToDomain(this ToDoNode node)
        {
            return new ToDoNodeModel
            {
                Id = node.Id,
                Name = node.Name,
                Date = node.Date,
                Description = node.Description,
                Status = node.Status,
                Type = node.Type,
            };
        }
    }
}
