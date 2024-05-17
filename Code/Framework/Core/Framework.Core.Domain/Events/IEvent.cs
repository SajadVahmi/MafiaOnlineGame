namespace Framework.Core.Domain.Events;

public interface IEvent
{
    Guid EventId { get; }
    DateTimeOffset TimeOfOccurrence { get; }
}