using Framework.Core.Domain.Events;
using Games.Domain.Contracts.Enums;

namespace Games.Domain.PlayerAggregate.DomainEvents;

public record PlayerGenderChanged(
    string EventId,
    string Id,
    Gender Gender,
    DateTimeOffset WhenItHappened)
    : DomainEvent(EventId, WhenItHappened);