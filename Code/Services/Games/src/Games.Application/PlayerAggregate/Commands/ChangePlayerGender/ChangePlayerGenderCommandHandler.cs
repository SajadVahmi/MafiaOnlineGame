using Framework.Core.Application.Commands;
using Framework.Core.Domain.ValueObjects;
using Framework.Core.ServiceContracts;
using Games.Application.PlayerAggregate.Factories;
using Games.Domain.PlayerAggregate.Contracts;

namespace Games.Application.PlayerAggregate.Commands.ChangePlayerGender;

public class ChangePlayerGenderCommandHandler(
    IPlayerRepository playerRepository,
    IIdGenerator idGenerator,
    IClock clock)
    : ICommandHandler<ChangePlayerGenderCommand>
{
    public async Task HandleAsync(ChangePlayerGenderCommand command, CancellationToken cancellationToken = default)
    {
        var player =await playerRepository.GetAsync(EntityId.Instantiate(command.PlayerId), cancellationToken);

        var changePlayerGenderArgs = PlayerArgsFactory.CreateChangeGenderArgs(
            command: command,
            idGenerator: idGenerator,
            clock: clock);

        player.ChangeGender(changePlayerGenderArgs);

        await playerRepository.UpdateAsync(player, cancellationToken);
    }
}