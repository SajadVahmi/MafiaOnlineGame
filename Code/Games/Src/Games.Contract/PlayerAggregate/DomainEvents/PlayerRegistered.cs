using Framework.Core.Domain.Events;
using Games.Contract._Shared.Enums;

namespace Games.Contract.PlayerAggregate.DomainEvents;

public record PlayerRegistered(
    string EventId,
    string Id,
    string FirstName,
    string LastName,
    Gender Gender,
    string UserId,
    DateTimeOffset WhenItHappened)
    : DomainEvent(EventId, WhenItHappened);