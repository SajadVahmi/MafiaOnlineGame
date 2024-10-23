namespace Framework.Core.Domain.Snapshots;

public class EmptySnapshot : ISnapshot
{
    public static ISnapshot Instance = new EmptySnapshot();
    public int Version { get; }
    private EmptySnapshot()
    {
        Version = 0;
    }
}