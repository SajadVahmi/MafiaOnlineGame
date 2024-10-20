using EventStore.Client;
using Framework.Core.Domain.Aggregates;
using Framework.Core.Domain.Events;
using Framework.Core.ServiceContracts;

namespace Framework.Persistence.EventStore
{
    public class EventStoreDb(EventStoreClient client, IEventTypeResolver typeResolver) : IEventStore
    {
        public async Task<List<IDomainEvent>> GetEventsOfStreamAsync<T, TKey>(TKey id,IJsonSerializerAdapter jsonSerializer, CancellationToken cancellationToken = default) where T : AggregateRoot<TKey> where TKey : notnull
        {
            if (id == null) throw new ArgumentNullException(nameof(id));
            return await GetEventsOfStreamAsync<T, TKey>(id, StreamPosition.Start.ToInt64(), jsonSerializer, cancellationToken);
        }
        public async Task<List<IDomainEvent>> GetEventsOfStreamAsync<T, TKey>(TKey id, long fromIndex,IJsonSerializerAdapter jsonSerializer,CancellationToken cancellationToken=default) where T : AggregateRoot<TKey> where TKey : notnull
        {
            if (id == null) throw new ArgumentNullException(nameof(id));
            var streamId = StreamNames.GetStreamName<T, TKey>(id);
            var streamEvents = await EventStreamReader.Read(client, streamId, fromIndex); //TODO:remove this hard-coded '200' and real all events
            return DomainEventFactory.Create(streamEvents, typeResolver, jsonSerializer);
        }

        public async Task AppendEventsAsync<T, TKey>(T aggregateRoot, IJsonSerializerAdapter jsonSerializer, CancellationToken cancellationToken = default) where T : AggregateRoot<TKey> where TKey : notnull
        {
            
                var events = aggregateRoot.GetEvents();
                var streamId = StreamNames.GetStreamName<T, TKey>(aggregateRoot.Id);
                var expectedVersion = ExpectedVersionCalculator.GetExpectedVersionOfAggregate(aggregateRoot);
                var eventData = EventDataFactory.CreateFromDomainEvents(events, jsonSerializer);
                await client.AppendToStreamAsync(streamId, expectedVersion, eventData,
                    cancellationToken: cancellationToken);
           
        }

    }
}