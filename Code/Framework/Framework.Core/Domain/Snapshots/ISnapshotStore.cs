using Framework.Core.Domain.Aggregates;

namespace Framework.Core.Domain.Snapshots;

public interface ISnapshotStore
{
    Task<ISnapshot> GetLatestSnapshotOf<T, TKey>(TKey id) where T : AggregateRoot<TKey>;
    Task<TSnapshot> GetLatestSnapshotOf<T, TKey, TSnapshot>(TKey id) where T : AggregateRoot<TKey>
        where TSnapshot : ISnapshot;

}