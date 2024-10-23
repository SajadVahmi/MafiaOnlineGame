using Framework.Core.Domain.Aggregates;
using Framework.Core.Domain.Events;
using Framework.Core.Domain.Snapshots;
using Framework.Core.ServiceContracts;

namespace Framework.Persistence.EventStore.Repositories
{
    public class EventSourceRepository<T, TKey>(
        IEventStore eventStore,
        IAggregateFactory aggregateFactory,
        ISnapshotStore snapshotStore,
        IJsonSerializerAdapter jsonSerializerAdapter)
        : IEventSourceRepository<T, TKey>
        where T : AggregateRoot<TKey>
        where TKey : notnull
    {
        public async Task<T> GetByIdAsync(TKey id, CancellationToken cancellationToken = default)
        {
            var snapshot = await snapshotStore.GetLatestSnapshotOf<T, TKey>(id);
            var listOfEvents = await eventStore.GetEventsOfStreamAsync<T,TKey>(id, snapshot.Version, jsonSerializerAdapter, cancellationToken);
            return aggregateFactory.Create<T>(listOfEvents, snapshot);
        }

        public async Task AppendEventsAsync(T aggregate, CancellationToken cancellationToken = default)
        {
            await eventStore.AppendEventsAsync<T,TKey>(aggregate, jsonSerializerAdapter, cancellationToken);
            //TODO: clear uncommitted events after append
            //TODO: if (snapshot store == InMemory) then  1.GetSnapshotFromAggregate  2.AddOrUpdateSnapshot(aggregate)
        }
    }
}