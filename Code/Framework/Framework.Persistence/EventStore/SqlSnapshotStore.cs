using Framework.Core.Domain.Aggregates;
using Framework.Core.Domain.Snapshots;

namespace Framework.Persistence.EventStore;

public class SqlSnapshotStore : ISnapshotStore
{
    public Task<ISnapshot> GetLatestSnapshotOf<T, TKey>(TKey id) where T : AggregateRoot<TKey> where TKey : notnull
    {
        //var streamId = "........";
        //.... Excute query return

        return Task.FromResult<ISnapshot>(EmptySnapshot.Instance);
    }

    public Task<TSnapshot> GetLatestSnapshotOf<T, TKey, TSnapshot>(TKey id) where T : AggregateRoot<TKey> where TSnapshot : ISnapshot where TKey : notnull
    {
        throw new NotImplementedException();
    }
}