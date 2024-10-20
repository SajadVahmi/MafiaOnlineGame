using Framework.Core.ServiceContracts;

namespace Framework.Core.Services;

public class GuidIdGenerator : IIdGenerator
{
    public string GetNewId()
    {
        return Guid.NewGuid().ToString();
    }
}
