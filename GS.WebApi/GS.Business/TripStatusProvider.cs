using GS.Business.Infrastructure;
using GS.Data.Entities;
using GS.Domain.Enums;

namespace GS.Business
{
    public class TripStatusProvider : ITripStatusProvider
    {
        private readonly IDateTimeProvider _dateTimeProvider;

        public TripStatusProvider(IDateTimeProvider dateTimeProvider)
        {
            _dateTimeProvider = dateTimeProvider;
        }

        public TripStatus GetStatus(Trip tripStatus)
        {
            var utcNow = _dateTimeProvider.GetUtcNow();

            if (!tripStatus.StartDate.HasValue || !tripStatus.EndDate.HasValue)
            {
                return TripStatus.Planned;
            }
            else
            {
                if (utcNow < tripStatus.StartDate.Value)
                {
                    return TripStatus.Planned;
                }
                else if (utcNow > tripStatus.EndDate.Value)
                {
                    return TripStatus.Closed;
                }
                else
                {
                    return TripStatus.InProgress;
                }
            }
        }
    }
}
