using Framework.Core.Application.Commands;
using Framework.Core.Contracts;
using Games.Application.PlayerAggregate.Dto;
using Games.Application.PlayerAggregate.Factories;
using Games.Domain.PlayerAggregate.Contracts;
using Games.Domain.PlayerAggregate.Models;

namespace Games.Application.PlayerAggregate.Commands.RegisterPlayer;

public class RegisterPlayerCommandHandler(
    IPlayerRepository playerRepository,
    IIdGenerator idGenerator,
    IClock clock,
    IAuthenticatedUser authenticatedUser,
    IMapperAdapter mapper)
    : ICommandHandler<RegisterPlayerCommand, RegisteredPlayerDto>
{

    public async Task<RegisteredPlayerDto> HandleAsync(RegisterPlayerCommand command, CancellationToken cancellationToken = default)
    {
        var registrationArgs = PlayerArgsFactory.CreateRegistrationArgs(
            command: command,
            idGenerator: idGenerator,
            clock: clock,
            authenticatedUser: authenticatedUser);

        var player = Player.Register(registrationArgs);

        await playerRepository.AddAsync(player, cancellationToken);

        return mapper.Map<RegisteredPlayerDto>(player);
    }
}