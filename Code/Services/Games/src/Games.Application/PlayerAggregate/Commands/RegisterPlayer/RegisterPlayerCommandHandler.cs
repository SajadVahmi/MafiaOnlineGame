using Framework.Core.Application.Commands;
using Framework.Core.ServiceContracts;
using Games.Application.PlayerAggregate.Factories;
using Games.Contract.PlayerAggregate.Dto;
using Games.Domain.PlayerAggregate.Contracts;
using Games.Domain.PlayerAggregate.Models;

namespace Games.Application.PlayerAggregate.Commands.RegisterPlayer;

public class RegisterPlayerCommandHandler(
    IPlayerRepository playerRepository,
    IIdGenerator idGenerator,
    IClock clock,
    IAuthenticatedUser authenticatedUser,
    IPlayerDuplicationRegistrationDetector duplicationRegistrationDetector,
    IMapperAdapter mapper)
    : ICommandHandler<RegisterPlayerCommand, RegisteredPlayerDto>
{

    public async Task<RegisteredPlayerDto> HandleAsync(RegisterPlayerCommand command, CancellationToken cancellationToken = default)
    {
        var registrationArgs = PlayerArgsFactory.CreateRegistrationArgs(
            command: command,
            idGenerator: idGenerator,
            clock: clock,
            duplicationRegistrationDetector: duplicationRegistrationDetector,
            authenticatedUser: authenticatedUser);

        var player =await Player.RegisterAsync(registrationArgs,cancellationToken);

        await playerRepository.AddAsync(player, cancellationToken);

        return mapper.Map<RegisteredPlayerDto>(player);
    }
}