using Framework.Core.ApplicationServices.Exceptions;
using Framework.Core.Contracts;
using Players.ApplicationServices.PlayerAggregate.Dto;
using Players.ApplicationServices.PlayerAggregate.Factories;
using Players.Contracts.Resources;
using Players.Domain.PlayerAggregate.Data;
using Players.Domain.PlayerAggregate.Models;
using Players.Domain.PlayerAggregate.Services;

namespace Players.ApplicationServices.PlayerAggregate.Services;

public class PlayerApplicationService(
    IPlayerRepository playerRepository,
    IDuplicateRegistrationCheckService duplicateRegistrationCheckService,
    IEventIdProvider eventIdProvider,
    IMapperAdapter mapper,
    IClock clock) : IPlayerApplicationService
{
    public async Task<RegisteredPlayerDto> RegisterAsync(PlayerRegistrationDto playerRegistrationDto, CancellationToken cancellationToken = default)
    {
        var playerRegisterArgs = await PlayerRegisterArgsFactory.CreateAsync(
            playerRegistrationDto: playerRegistrationDto,
            playerRepository: playerRepository,
            duplicateRegistrationCheckService: duplicateRegistrationCheckService,
            eventIdProvider: eventIdProvider,
            clock: clock,
            cancellationToken: cancellationToken);

        var player = await Player.RegisterAsync(playerRegisterArgs, cancellationToken);

        await playerRepository.RegisterAsync(player, cancellationToken);

        var registeredPlayerDto = mapper.Map<Player, RegisteredPlayerDto>(player);

        return registeredPlayerDto;
    }

    public async Task ChangeProfileAsync(PlayerChangeProfileDto playerChangeProfileDto, CancellationToken cancellationToken = default)
    {
        var player = await playerRepository.LoadAsync(
            playerId: PlayerId.Instantiate(playerChangeProfileDto.Id),
            userId: playerChangeProfileDto.UserId, cancellationToken: cancellationToken);

        if (player is null)
            throw new NotFoundException(
                message: PlayersResource.Player101ThePlayerNotFound,
                code: PlayersCodes.Player101ThePlayerNotFound,
                name: PlayersResource.Player101ThePlayerNotFound);

        var playerChangeProfile = PlayerChangeProfileArgsFactory.Create(
            playerChangeProfileDto: playerChangeProfileDto,
            eventIdProvider: eventIdProvider,
            clock: clock);

        player.ChangeProfile(playerChangeProfile);

        await playerRepository.SaveChangesAsync();
    }

    public async Task<PlayerDto> ViewAsync(long playerId, string userId, CancellationToken cancellationToken = default)
    {
        var player = await playerRepository.ViewAsync(
            playerId: PlayerId.Instantiate(playerId),
            userId: userId,
            cancellationToken: cancellationToken);

        if (player is null) throw new NotFoundException(
            message: PlayersResource.Player101ThePlayerNotFound,
            code: PlayersCodes.Player101ThePlayerNotFound,
            name: PlayersResource.Player101ThePlayerNotFound);

        return mapper.Map<Player, PlayerDto>(player);

    }
}
