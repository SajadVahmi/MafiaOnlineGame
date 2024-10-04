using Framework.Core.Domain.Events;
using Games.Domain.Contracts.Enums;

namespace Games.Domain.PlayerAggregate.DomainEvents;

public record PlayerRegistered(
    string EventId,
    string Id,
    string FirstName,
    string LastName,
    Gender Gender,
    string UserId,
    DateTimeOffset WhenItHappened)
    : DomainEvent(EventId, WhenItHappened);