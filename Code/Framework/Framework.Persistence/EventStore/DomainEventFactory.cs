using EventStore.Client;
using Framework.Core.Domain.Events;
using Framework.Core.ServiceContracts;
using Framework.Persistence.EventStore.Mappings;
using Newtonsoft.Json.Linq;
using System.Text;

namespace Framework.Persistence.EventStore
{
    internal static class DomainEventFactory
    {
        public static List<IDomainEvent> Create(List<ResolvedEvent> events, IEventTypeResolver typeResolver, IJsonSerializerAdapter jsonSerializer)
        {
            if (!events.Any())
                return new();

            return events.Select(a => Create(a, typeResolver, jsonSerializer)).ToList();

        }

        public static IDomainEvent Create(ResolvedEvent @event, IEventTypeResolver typeResolver, IJsonSerializerAdapter jsonSerializer)
        {
            var type = typeResolver.GetType(@event.Event.EventType);
            if (type == null)
                throw new Exception("Cannot resolve type of event");
            var body = Encoding.UTF8.GetString(@event.Event.Data.ToArray());
            body = ApplyMappings(body, type);
            return ((IDomainEvent)jsonSerializer.Deserialize(body, type)!);
        }

        private static string ApplyMappings(string body, Type type)
        {
            var filter = SchemaMappingRegistration.GetFilterForType(type);
            if (filter is null) return body;
            var json = JObject.Parse(body);
            json = filter.Apply(json);
            return json.ToString();
        }
    }
}