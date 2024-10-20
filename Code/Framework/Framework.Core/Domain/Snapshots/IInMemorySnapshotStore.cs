using Framework.Core.Domain.Aggregates;

namespace Framework.Core.Domain.Snapshots;

public interface IInMemorySnapshotStore : ISnapshotStore
{
    Task AddOrUpdateSnapshot<T, TKey>(TKey id, ISnapshot snapshot) where T : AggregateRoot<TKey>;
}