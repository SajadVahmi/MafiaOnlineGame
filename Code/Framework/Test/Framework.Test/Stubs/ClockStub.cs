using Framework.Core.Contracts;

namespace Framework.Test.Stubs;

public class ClockStub : IClock
{
    public static ClockStub Instantiate() => new();

    public static ClockStub InstantiateOn(DateTimeOffset now) => new(now);



    private DateTimeOffset _now;

    protected ClockStub() =>
    _now = DateTimeOffset.UtcNow;

    protected ClockStub(DateTimeOffset now) =>
        _now = now;

    public DateTimeOffset Now() =>
         _now;

    public void TimeTravelTo(DateTimeOffset targetTime) =>
        _now = targetTime;

   
}
