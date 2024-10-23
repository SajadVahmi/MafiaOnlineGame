using Framework.Core.Domain.Events;
using Games.Contract.Enums;

namespace Games.Contract.DomainEvents;

public record PlayerRegistered(
    string EventId,
    string Id,
    string FirstName,
    string LastName,
    Gender Gender,
    string UserId,
    DateTimeOffset WhenItHappened)
    : DomainEvent(EventId, WhenItHappened);