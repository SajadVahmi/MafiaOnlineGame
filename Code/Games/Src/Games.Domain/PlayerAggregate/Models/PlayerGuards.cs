using Games.Domain.PlayerAggregate.Exceptions;
using Games.Domain.PlayerAggregate.Services;

namespace Games.Domain.PlayerAggregate.Models;

public class PlayerGuards
{
    public static async Task AvoidDoubleRegistrationAsync(Player player,IDuplicateRegistrationCheckService duplicateRegistrationCheckService ,CancellationToken cancellationToken = default)
    {
        if (await duplicateRegistrationCheckService.CheckAsync(player.UserId, cancellationToken))
            throw new PlayerHasBeenRegisteredForThisUserIdException();
    }
}