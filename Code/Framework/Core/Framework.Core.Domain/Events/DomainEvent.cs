namespace Framework.Core.Domain.Events;

public abstract class DomainEvent(string id, DateTimeOffset whenItHappened) : IDomainEvent
{
    public string EventId { get; private set; } = id;

    public DateTimeOffset TimeOfOccurrence { get; private set; } = whenItHappened;
}
