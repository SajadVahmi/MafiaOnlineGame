using Framework.Core.Contracts;

namespace Framework.Test.Stubs;

public class EventIdProviderStub : IEventIdProvider
{
    private string _id;

    public static EventIdProviderStub Instantiate() =>
        new EventIdProviderStub();

    public EventIdProviderStub() =>
        _id = Guid.NewGuid().ToString();


    public EventIdProviderStub(string id) => _id = id;


    public string Get()
    {
        return _id;
    }
}
