using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Framework.Core.Domain.Events;
using Games.Domain.Contracts.Enums;

namespace Games.Domain.Contracts.DomainEvents.PlayerAggregate;

public record PlayerRegistered(
    Guid EventId,
    DateTimeOffset WhenItHappened,
    string Id,
    string Name,
    string Family,
    Gender Gender,
    string UserId)
    : DomainEvent(EventId, WhenItHappened);