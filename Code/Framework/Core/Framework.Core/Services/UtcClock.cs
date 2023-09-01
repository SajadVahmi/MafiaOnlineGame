using Framework.Core.Contracts;

namespace Framework.Core.Services;

public class UtcClock : IClock
{
    public DateTimeOffset Now()
    {
        return DateTimeOffset.UtcNow;
    }
}