using System;

namespace GS.Domain.Models.ItemToTake
{
    public class ItemToTakeModel : ItemToTakeBaseModel
    {
        public Guid Id { get; set; }

        public bool IsTaken { get; set; }
    }
}
