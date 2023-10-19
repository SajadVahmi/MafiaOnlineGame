using Framework.Core.Contracts;
using Framework.Core.Domian.Aggregates;
using Framework.Core.Domian.Events;

namespace Framework.Infra.OutBox.Models;

public static class OutBoxEventItemFactory
{
    public static List<OutBoxEventItem> Create(IAggregateRoot aggregateRoot,IEnumerable<IDomainEvent> domainEvents,IObjectSerializer serializer,IAuthenticatedUser authenticatedUser)
    {
        return domainEvents.Select(domainEvent=>Create(aggregateRoot,domainEvent,serializer,authenticatedUser)).ToList();
    }

    private static OutBoxEventItem Create(IAggregateRoot aggregateRoot,IDomainEvent domainEvent, IObjectSerializer serializer, IAuthenticatedUser authenticatedUser)
    {
        return new OutBoxEventItem()
        {
            EventId = domainEvent.EventId,
            AccuredByUserId = authenticatedUser.UserId(),
            AccuredOn =domainEvent.WhenItHappened,
            AggregateName = aggregateRoot.GetType().Name,
            AggregateTypeName = aggregateRoot.GetType().FullName,
            EventName = domainEvent.GetType().Name,
            EventTypeName = domainEvent.GetType().FullName,
            EventPayload = serializer.Serilize(domainEvent)
        };
    }
}