using Framework.Core.Domain.Events;
using Games.Domain.PlayerAggregate.DomainEvents;

namespace Backgrounds.Projection.Sql.Handlers;

public class PlayerRenamedHandler : IEventHandler<PlayerRenamed>
{
    public Task Handle(PlayerRenamed @event)
    {
        return Task.CompletedTask;
    }
}