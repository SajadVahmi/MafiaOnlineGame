﻿using System.Text;
using EventStore.ClientAPI;
using Framework.Core.Contracts;
using Framework.Core.Domain.Events;
using Framework.Persistence.EventStore.Mappings;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Framework.Persistence.EventStore
{
    internal static class DomainEventFactory
    {
        public static List<IDomainEvent> Create(List<ResolvedEvent> events, IEventTypeResolver typeResolver,IJsonSerializerAdapter jsonSerializer)
        {
            return events.Select(a=> Create(a, typeResolver, jsonSerializer)).ToList();
        }

        public static IDomainEvent Create(ResolvedEvent @event, IEventTypeResolver typeResolver,IJsonSerializerAdapter jsonSerializer)
        {
            var type = typeResolver.GetType(@event.Event.EventType);
            var body = Encoding.UTF8.GetString(@event.Event.Data);
            body = ApplyMappings(body, type);
            return (IDomainEvent)jsonSerializer.Deserialize(body, type);
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