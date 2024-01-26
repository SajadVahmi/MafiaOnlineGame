namespace Framework.Core.Domain.Events;

public interface IEvent
{
    string EventId { get; }
    DateTimeOffset TimeOfOccurrence { get; }
}