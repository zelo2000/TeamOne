using GS.Business.Infrastructure;
using System;

namespace GS.Business
{
    public class DateTimeProvider : IDateTimeProvider
    {
        public DateTime GetUtcNow()
        {
            return DateTime.Now;
        }
    }
}
