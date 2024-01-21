using Framework.Core.ApplicationServices.Exceptions;
using Framework.Core.Contracts;
using Players.ApplicationServices.PlayerAggregate.Dtos;
using Players.ApplicationServices.PlayerAggregate.Factories;
using Players.Contracts.Resources;
using Players.Domain.PlayerAggregate.Data;
using Players.Domain.PlayerAggregate.Models;
using Players.Domain.PlayerAggregate.Services;

namespace Players.ApplicationServices.PlayerAggregate.Services;

public class PlayerApplicationService : IPlayerApplicationService
{
    private readonly IPlayerRepository _playerRepository;

    private readonly IDuplicateRegistrationCheckService _duplicateRegistrationCheckService;

    private readonly IEventIdProvider _eventIdProvider;

    private readonly IMapperAdapter _mapper;

    private readonly IClock _clock;

    public PlayerApplicationService(IPlayerRepository playerRepository,
        IDuplicateRegistrationCheckService duplicateRegistrationCheckService,
        IEventIdProvider eventIdProvider,
        IMapperAdapter mapper,
        IClock clock)
    {
        _playerRepository = playerRepository;

        _duplicateRegistrationCheckService = duplicateRegistrationCheckService;

        _eventIdProvider = eventIdProvider;

        _mapper = mapper;

        _clock = clock;
    }

    public async Task<RegisteredPlayerDto> RegisterAsync(PlayerRegistrationDto playerRegistrationDto, CancellationToken cancellationToken = default)
    {
        PlayerRegisterArgs playerRegisterArgs = await PlayerRegisterArgsFactory.CreateAsync(
            playerRegistrationDto: playerRegistrationDto,
            playerRepository: _playerRepository,
            duplicateRegistrationCheckService: _duplicateRegistrationCheckService,
            eventIdProvider: _eventIdProvider,
            clock: _clock,
            cancellationToken: cancellationToken);

        Player player = await Player.RegisterAsync(playerRegisterArgs, cancellationToken);

        await _playerRepository.RegisterAsync(player, cancellationToken);

        RegisteredPlayerDto registeredPlayerDto = _mapper.Map<Player, RegisteredPlayerDto>(player);

        return registeredPlayerDto;
    }

    public async Task ChangeProfileAsync(PlayerChangeProfileDto playerChangeProfileDto, CancellationToken cancellationToken = default)
    {
        var player = await _playerRepository.LoadAsync(
            playerId: PlayerId.Instantiate(playerChangeProfileDto.Id),
            userId: playerChangeProfileDto.UserId);

        if (player is null)
            throw new AggregateNotFoundException(
                message:PlayersResource.Player101ThePlayerNotFound,
                code:PlayersCodes.Player101ThePlayerNotFound,
                name:PlayersResource.Player101ThePlayerNotFound);

      var playerChangeProfile= PlayerChangeProfileArgsFactory.Create(
            playerChangeProfileDto: playerChangeProfileDto,
            eventIdProvider: _eventIdProvider,
            clock: _clock);

        player.ChangeProfile(playerChangeProfile);

        await _playerRepository.SaveChangesAsync();
    }
}
