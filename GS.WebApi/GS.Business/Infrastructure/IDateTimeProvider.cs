using System;

namespace GS.Business.Infrastructure
{
    public interface IDateTimeProvider
    {
        DateTime GetUtcNow();
    }
}
