using EventStore.Client;
using Framework.Core.Domain.Events;
using Framework.Core.ServiceContracts;
using System.Text;

namespace Framework.Persistence.EventStore
{
    internal static class EventDataFactory
    {
        public static EventData CreateFromDomainEvent(IDomainEvent domainEvent,IJsonSerializerAdapter jsonSerializerAdapter)
        {
            var data = jsonSerializerAdapter.Serialize(domainEvent);
            return new EventData(
                eventId: Uuid.FromGuid(Guid.Parse(domainEvent.EventId)), 
                type: domainEvent.GetType().Name,
                data: Encoding.UTF8.GetBytes(data ?? throw new InvalidOperationException()),
                metadata: new byte[] {}
            );
        }

        public static List<EventData> CreateFromDomainEvents(IEnumerable<IDomainEvent> domainEvent,IJsonSerializerAdapter jsonSerializer)
        {
            return domainEvent.Select(e=>CreateFromDomainEvent(e, jsonSerializer)).ToList();
        }
    }
}
