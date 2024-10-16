namespace Framework.Core.Domain.Events;

public abstract record DomainEvent(string EventId, DateTimeOffset WhenItHappened) : IDomainEvent
{
    public string EventId { get; private set; } = EventId;

    public DateTimeOffset TimeOfOccurrence { get; private set; } = WhenItHappened;
}
