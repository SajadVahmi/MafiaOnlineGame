using Framework.Core.Contracts;

namespace Framework.Core.Services;

public class GuidEventIdProvider : IEventIdProvider
{
    public string Get()
    {
        return Guid.NewGuid().ToString();
    }
}
