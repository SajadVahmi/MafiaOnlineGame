namespace Framework.Core.Contracts;

public interface IIdGenerator
{
    public string GetNewString();
    public Guid GetNewGuid();
}
