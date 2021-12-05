using GS.Data.Entities;
using GS.Domain.Models.ItemToTake;

namespace GS.Business.Mapping
{
    public static class ItemToTakeMapper
    {
        public static ItemToTake ToEntity(this ItemToTakeBaseModel model)
        {
            return new ItemToTake
            {
                Name = model.Name
            };
        }

        public static ItemToTakeModel ToDomain(this ItemToTake itemToTake)
        {
            return new ItemToTakeModel
            {
                Id = itemToTake.Id,
                Name = itemToTake.Name,
                IsTaken = itemToTake.IsTaken,
            };
        }
    }
}
