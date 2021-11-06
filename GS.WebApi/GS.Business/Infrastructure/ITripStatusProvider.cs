using GS.Data.Entities;
using GS.Domain.Enums;

namespace GS.Business.Infrastructure
{
    public interface ITripStatusProvider
    {
        TripStatus GetStatus(Trip tripStatus);
    }
}
