using Framework.Core.Domain.Events;
using Games.Contract._Shared.Enums;

namespace Games.Contract.PlayerAggregate.DomainEvents;

public record PlayerGenderChanged(
    string EventId,
    string Id,
    Gender Gender,
    DateTimeOffset WhenItHappened)
    : DomainEvent(EventId, WhenItHappened);