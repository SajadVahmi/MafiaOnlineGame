using Framework.Core.Contracts;

namespace Framework.Test.Stubs;

public class EventIdProviderStub(string id) : IEventIdProvider
{
    public static EventIdProviderStub Instantiate() =>
        new EventIdProviderStub();

    public EventIdProviderStub() : this(Guid.NewGuid().ToString())
    {
    }


    public string Get()
    {
        return id;
    }
}
