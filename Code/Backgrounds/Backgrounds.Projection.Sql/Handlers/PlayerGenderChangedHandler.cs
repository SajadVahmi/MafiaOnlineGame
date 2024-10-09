using Framework.Core.Domain.Events;
using Games.Domain.PlayerAggregate.DomainEvents;

namespace Backgrounds.Projection.Sql.Handlers;

public class PlayerGenderChangedHandler:IEventHandler<PlayerGenderChanged>
{
    public Task Handle(PlayerGenderChanged @event)
    {
        return Task.CompletedTask;
    }
}