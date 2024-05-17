using Framework.Core.Domain.Aggregates;

namespace Framework.Persistence.EventStore;

public static class StreamNames
{
    public static string GetStreamName<T, TKey>(TKey id) where T : AggregateRoot<TKey>
    {
        var type = typeof(T).Name;
        return $"{type}-{id}";
    }
}