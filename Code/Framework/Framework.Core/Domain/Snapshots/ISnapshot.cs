namespace Framework.Core.Domain.Snapshots;

public interface ISnapshot
{
    int Version { get; }
}