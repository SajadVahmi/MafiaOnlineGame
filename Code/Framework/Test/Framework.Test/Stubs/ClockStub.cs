using Framework.Core.Contracts;

namespace Framework.Test.Stubs;

public class ClockStub : IClock
{
    public static ClockStub Instantiate() => new();

    private DateTimeOffset _now;

    public ClockStub() =>
    _now = DateTimeOffset.UtcNow;

    public ClockStub(DateTimeOffset now) =>
        _now = now;

    public DateTimeOffset Now() =>
         _now;

    public void TimeTravelTo(DateTimeOffset targetTime) =>
        _now = targetTime;

    public void TimeTravelTo(string targetTime)
    {
        throw new NotImplementedException();
    }
}
