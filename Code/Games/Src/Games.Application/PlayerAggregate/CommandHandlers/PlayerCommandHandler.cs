﻿using Framework.Core.Application.Commands;
using Framework.Core.Contracts;
using Games.Application.PlayerAggregate.Commands;
using Games.Application.PlayerAggregate.Factories;
using Games.Domain.PlayerAggregate.Data;
using Games.Domain.PlayerAggregate.Models;
using Games.Domain.PlayerAggregate.Services;

namespace Games.Application.PlayerAggregate.CommandHandlers;

public class PlayerCommandHandler(
    IPlayerRepository playerRepository,
    IIdGenerator idGenerator,
    IClock clock,
    IAuthenticatedUser authenticatedUser)
    : ICommandHandler<RegisterPlayerCommand>
{

    public async Task HandleAsync(RegisterPlayerCommand command, CancellationToken cancellationToken = default)
    {
        var registrationArgs = PlayerArgsFactory.CreateRegistrationArgs(
            command: command,
            idGenerator: idGenerator,
            clock: clock,
            authenticatedUser: authenticatedUser);

        var player = Player.Register(registrationArgs);

        await playerRepository.AddAsync(player, cancellationToken);
    }
}