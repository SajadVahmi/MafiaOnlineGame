using Framework.Core.Contracts;
using Games.Application.Contracts.PlayerAggregate.Commands;
using Games.Domain.PlayerAggregate.Arguments;
using Games.Domain.PlayerAggregate.Services;

namespace Games.Application.PlayerAggregate.Factories;

public static class PlayerArgsFactory
{
    public static PlayerRegistrationArgs CreateRegistrationArgs(RegisterPlayerCommand command, IIdGenerator idGenerator,
        IAuthenticatedUser authenticatedUser,IClock clock, IDuplicateRegistrationCheckService duplicateRegistrationCheckService)
    {
        return new PlayerRegistrationArgs()
        {
            Name = command.Name,
            Family = command.Family,
            UserId = authenticatedUser.GetSub()!,
            Clock = clock,
            IdGenerator = idGenerator,
            DuplicateRegistrationCheckService = duplicateRegistrationCheckService
        };
    }
}