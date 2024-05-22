using Framework.Core.Contracts;
using Framework.Core.Domain.Aggregates;
using Framework.Core.Domain.Events;
using Framework.Core.Domain.Snapshots;

namespace Framework.Persistence.EventStore.Repositories
{
    public class EventSourceRepository<T, TKey> : IEventSourceRepository<T, TKey> where T : AggregateRoot<TKey> where TKey : notnull
    {
        private readonly IEventStore _eventStore;
        private readonly IAggregateFactory _aggregateFactory;
        private readonly ISnapshotStore _snapshotStore;
        private readonly IJsonSerializerAdapter _jsonSerializerAdapter;

        public EventSourceRepository(IEventStore eventStore, IAggregateFactory aggregateFactory, ISnapshotStore snapshotStore,IJsonSerializerAdapter jsonSerializerAdapter)
        {
            _eventStore = eventStore;
            _aggregateFactory = aggregateFactory;
            _snapshotStore = snapshotStore;
            _jsonSerializerAdapter = jsonSerializerAdapter;
        }
        public async Task<T> GetByIdAsync(TKey id, CancellationToken cancellationToken = default)
        {
            var snapshot = await _snapshotStore.GetLatestSnapshotOf<T, TKey>(id);
            var listOfEvents = await _eventStore.GetEventsOfStreamAsync<T,TKey>(id, snapshot.Version, _jsonSerializerAdapter);
            return _aggregateFactory.Create<T>(listOfEvents, snapshot);
        }

        public async Task AppendEventsAsync(T aggregate, CancellationToken cancellationToken = default)
        {
            await _eventStore.AppendEventsAsync<T,TKey>(aggregate, _jsonSerializerAdapter);
            //TODO: clear uncommitted events after append
            //TODO: if (snapshot store == InMemory) then  1.GetSnapshotFromAggregate  2.AddOrUpdateSnapshot(aggregate)
        }
    }
}