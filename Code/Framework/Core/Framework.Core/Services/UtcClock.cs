using Framework.Core.Contracts;

namespace Framework.Core.Services;

public class UtcClock : IClock
{
    public DateTimeOffset Now()
    {
        return DateTimeOffset.UtcNow;
    }

    public void TimeTravelTo(DateTimeOffset targetTime)
    {
        throw new NotImplementedException();
    }

    public void TimeTravelTo(string targetTime)
    {
        throw new NotImplementedException();
    }
}