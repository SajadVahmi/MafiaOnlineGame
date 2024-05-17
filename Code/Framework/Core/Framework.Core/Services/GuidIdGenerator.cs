using Framework.Core.Contracts;
namespace Framework.Core.Services;

public class GuidIdGenerator : IIdGenerator
{
    public Guid GetNewGuid()
    {
        return Guid.NewGuid();
    }

    public string GetNewString()
    {
        return Guid.NewGuid().ToString();
    }
}
