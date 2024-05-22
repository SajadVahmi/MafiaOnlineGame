using Framework.Core.Application.Commands;
using Framework.Core.Contracts;
using Games.Application.Contracts.PlayerAggregate.Commands;
using Games.Application.PlayerAggregate.Factories;
using Games.Domain.PlayerAggregate.Data;
using Games.Domain.PlayerAggregate.Models;
using Games.Domain.PlayerAggregate.Services;

namespace Games.Application.PlayerAggregate.CommandHandlers;

public class PlayerCommandHandler:ICommandHandler<RegisterPlayerCommand>
{
    private readonly IPlayerRepository _playerRepository;
    private readonly IIdGenerator _idGenerator;
    private readonly IClock _clock;
    private readonly IDuplicateRegistrationCheckService _duplicateRegistrationCheckService;
    private readonly IAuthenticatedUser _authenticatedUser;

    public PlayerCommandHandler(IPlayerRepository playerRepository,IIdGenerator idGenerator,IClock clock,IDuplicateRegistrationCheckService duplicateRegistrationCheckService,IAuthenticatedUser authenticatedUser)
    {
        _playerRepository = playerRepository;
        _idGenerator = idGenerator;
        _clock = clock;
        _duplicateRegistrationCheckService = duplicateRegistrationCheckService;
        _authenticatedUser = authenticatedUser;
    }
    public async Task HandleAsync(RegisterPlayerCommand command, CancellationToken cancellationToken = default)
    {
        var registrationArgs = PlayerArgsFactory.CreateRegistrationArgs(
            command: command,
            idGenerator: _idGenerator,
            clock: _clock,
            duplicateRegistrationCheckService: _duplicateRegistrationCheckService,
            authenticatedUser: _authenticatedUser);

        var player = await Player.RegisterAsync(registrationArgs, cancellationToken);

        await _playerRepository.AddAsync(player, cancellationToken);
    }
}