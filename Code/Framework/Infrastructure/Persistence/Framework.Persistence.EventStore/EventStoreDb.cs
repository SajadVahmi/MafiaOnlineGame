using EventStore.ClientAPI;
using Framework.Core.Contracts;
using Framework.Core.Domain.Aggregates;
using Framework.Core.Domain.Events;

namespace Framework.Persistence.EventStore
{
    public class EventStoreDb : IEventStore
    {
        private readonly IEventStoreConnection _connection;
        private readonly IEventTypeResolver _typeResolver;
        public EventStoreDb(IEventStoreConnection connection, IEventTypeResolver typeResolver)
        {
            _connection = connection;
            _typeResolver = typeResolver;
        }
        public async Task<List<IDomainEvent>> GetEventsOfStream<T, TKey>(TKey id,IJsonSerializerAdapter jsonSerializer) where T : AggregateRoot<TKey>
        {
            return await GetEventsOfStream<T, TKey>(id, StreamPosition.Start, jsonSerializer);

        }
        public async Task<List<IDomainEvent>> GetEventsOfStream<T, TKey>(TKey id, int fromIndex,IJsonSerializerAdapter jsonSerializer) where T : AggregateRoot<TKey>
        {
            var streamId = StreamNames.GetStreamName<T, TKey>(id);
            var streamEvents = await EventStreamReader.Read(_connection, streamId, fromIndex, 200); //TODO:remove this hard-coded '200' and real all events
            return DomainEventFactory.Create(streamEvents, _typeResolver, jsonSerializer);
        }

        public async Task AppendEvents<T, TKey>(T aggregateRoot, IJsonSerializerAdapter jsonSerializer) where T : AggregateRoot<TKey>
        {
            var events = aggregateRoot.GetEvents();
            var streamId = StreamNames.GetStreamName<T, TKey>(aggregateRoot.Id);
            var expectedVersion = ExpectedVersionCalculator.GetExpectedVersionOfAggregate(aggregateRoot);
            var eventData = EventDataFactory.CreateFromDomainEvents(events,jsonSerializer);
            await _connection.AppendToStreamAsync(streamId, expectedVersion, eventData);
        }

    }
}