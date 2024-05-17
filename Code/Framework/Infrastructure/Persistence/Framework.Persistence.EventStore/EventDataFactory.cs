using EventStore.ClientAPI;
using Framework.Core.Contracts;
using Framework.Core.Domain.Events;
using System.Text;

namespace Framework.Persistence.EventStore
{
    internal static class EventDataFactory
    {
        public static EventData CreateFromDomainEvent(IDomainEvent domainEvent,IJsonSerializerAdapter jsonSerializerAdapter)
        {
            var data = jsonSerializerAdapter.Serialize(domainEvent);
            return new EventData(
                eventId: domainEvent.EventId, 
                type: domainEvent.GetType().Name,
                isJson: true,
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
