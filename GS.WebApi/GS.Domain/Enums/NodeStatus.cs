using System.ComponentModel.DataAnnotations;

namespace GS.Domain.Enums
{
    public enum NodeStatus
    {
        [Display(Name = "To Do")]
        ToDo = 1,

        [Display(Name = "In Progress")]
        InProgress = 2,

        [Display(Name = "Done")]
        Done = 3
    }
}
