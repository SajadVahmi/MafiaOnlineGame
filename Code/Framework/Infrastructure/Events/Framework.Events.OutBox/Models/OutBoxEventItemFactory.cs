using Framework.Core.Contracts;
using Framework.Core.Domain.Aggregates;
using Framework.Core.Domain.Events;


namespace Framework.Events.OutBox.Models;

public static class OutBoxEventItemFactory
{
    public static List<OutBoxEventItem> Create(IAggregateRoot aggregateRoot, IEnumerable<IDomainEvent> domainEvents, IJsonSerializerAdapter serializer, IAuthenticatedUser authenticatedUser)
    {
        return domainEvents.Select(domainEvent => Create(aggregateRoot, domainEvent, serializer, authenticatedUser)).ToList();
    }

    private static OutBoxEventItem Create(IAggregateRoot aggregateRoot, IDomainEvent domainEvent, IJsonSerializerAdapter serializer, IAuthenticatedUser authenticatedUser)
    {
        return new OutBoxEventItem()
        {
            EventId = domainEvent.EventId,
            OccurredByUserId = authenticatedUser.GetSub(),
            OccurredOn = domainEvent.TimeOfOccurrence,
            AggregateName = aggregateRoot.GetType().Name,
            AggregateTypeName = aggregateRoot.GetType().FullName,
            EventName = domainEvent.GetType().Name,
            EventTypeName = domainEvent.GetType().FullName,
            EventPayload = serializer.Serialize(domainEvent)
        };
    }
}
