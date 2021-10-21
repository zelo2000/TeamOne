using System.ComponentModel.DataAnnotations;

namespace GS.Domain.Enums
{
    public enum TripStatus
    {
        [Display(Name = "Planned")]
        Planned = 1,

        [Display(Name = "In Progress")]
        InProgress = 2,

        [Display(Name = "Closed")]
        Closed = 3
    }
}
