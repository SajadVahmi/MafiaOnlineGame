using Framework.Core.Domain.Events;
using Games.Domain.PlayerAggregate.DomainEvents;

namespace Backgrounds.Projection.Sql.Handlers;

public class PlayerRegisteredHandler:IEventHandler<PlayerRegistered>
{
    public Task Handle(PlayerRegistered @event)
    {
        return Task.CompletedTask;
    }
}