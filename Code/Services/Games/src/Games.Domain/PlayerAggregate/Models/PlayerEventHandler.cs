using Framework.Core.Domain.ValueObjects;
using Games.Contract.PlayerAggregate.DomainEvents;

namespace Games.Domain.PlayerAggregate.Models;

public partial class Player
{
    void When(PlayerRegistered @event)
    {
        Id = EntityId.Instantiate(@event.Id);

        Name=PlayerName.Instantiate(
            firstName: @event.FirstName,
            lastName: @event.LastName);

        Gender = @event.Gender;

        UserId = PlayerUserId.Instantiate(@event.UserId);
    }

    void When(PlayerRenamed @event)
    {
        Name = PlayerName.Instantiate(
            firstName: @event.FirstName,
            lastName: @event.LastName);

    }

    void When(PlayerGenderChanged @event)
    {
        Gender = @event.Gender;

    }
}