namespace Framework.Core.Domain.Events;

public abstract record DomainEvent(Guid EventId, DateTimeOffset WhenItHappened) : IDomainEvent
{
    public Guid EventId { get; private set; } = EventId;

    public DateTimeOffset TimeOfOccurrence { get; private set; } = WhenItHappened;
}
