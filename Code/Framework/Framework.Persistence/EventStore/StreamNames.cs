using Framework.Core.Domain.Aggregates;
using Framework.Core.Domain.ValueObjects;

namespace Framework.Persistence.EventStore;

public static class StreamNames
{
    public static string GetStreamName<T, TKey>(TKey id) where T : AggregateRoot<TKey> where TKey :notnull
    {
        var type = typeof(T).Name;
        return $"{type}-{id}";
    }
}