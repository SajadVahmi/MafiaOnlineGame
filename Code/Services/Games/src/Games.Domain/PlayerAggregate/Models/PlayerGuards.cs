using Framework.Core.Domain.Exceptions;
using Games.Contract._Shared.Resources;
using Games.Domain.PlayerAggregate.Arguments;

namespace Games.Domain.PlayerAggregate.Models;

public static class PlayerGuards
{
    public static async Task AvoidDuplicationRegistration(PlayerRegistrationArgs args,
        CancellationToken cancellationToken = default)
    {
        if (await args.DuplicationRegistrationDetector.DuplicateRegistrationIsGoingToHappenAsync(args.UserId, cancellationToken))
            throw new BusinessException(Exceptions.PlayerIsAlreadyRegistered);
    }
}