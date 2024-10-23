using Framework.Core.Application.Commands;
using Framework.Core.Domain.ValueObjects;
using Framework.Core.ServiceContracts;
using Games.Application.PlayerAggregate.Factories;
using Games.Domain.PlayerAggregate.Contracts;

namespace Games.Application.PlayerAggregate.Commands.RenamePlayer;

public class RenamePlayerCommandHandler(
    IPlayerRepository playerRepository,
    IIdGenerator idGenerator,
    IClock clock)
    : ICommandHandler<RenamePlayerCommand>
{
    public async Task HandleAsync(RenamePlayerCommand command, CancellationToken cancellationToken = default)
    {
        var player = await playerRepository.GetAsync(EntityId.Instantiate(command.PlayerId), cancellationToken);

        var renameArgs = PlayerArgsFactory.CreateRenameArgs(
            command: command,
            idGenerator: idGenerator,
            clock: clock);

        player.Rename(renameArgs);

        await playerRepository.UpdateAsync(player, cancellationToken);
    }
}