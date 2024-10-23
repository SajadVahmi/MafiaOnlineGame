using Framework.Core.Domain.Events;

namespace Games.Contract.DomainEvents;

public record PlayerRenamed(
    string EventId,
    string Id,
    string FirstName,
    string LastName,
    DateTimeOffset WhenItHappened)
    : DomainEvent(EventId, WhenItHappened);