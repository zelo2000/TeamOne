using System.ComponentModel.DataAnnotations;

namespace GS.Domain.Enums
{
    public enum NodeType
    {
        [Display(Name = "Before Trip")]
        Before = 1,

        [Display(Name = "At Trip")]
        At = 2,

        [Display(Name = "After Trip")]
        After = 3
    }
}
