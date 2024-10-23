using Framework.Core.Domain.Events;
using Games.Contract.Enums;

namespace Games.Contract.DomainEvents;

public record PlayerGenderChanged(
    string EventId,
    string Id,
    Gender Gender,
    DateTimeOffset WhenItHappened)
    : DomainEvent(EventId, WhenItHappened);