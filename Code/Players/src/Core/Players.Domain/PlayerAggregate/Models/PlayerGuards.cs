using Players.Domain.PlayerAggregate.Exceptions;

namespace Players.Domain.PlayerAggregate.Models;

public class PlayerGuards
{
    public static async Task AvoidDoubleRegistrationAsync(PlayerRegisterArgs args, CancellationToken cancellationToken = default)
    {
        if (await args.DuplicateRegistrationCheckService.CheckAsync(args.UserId, cancellationToken))

            throw new TheUserAlreadyRegistredException();
    }
}
