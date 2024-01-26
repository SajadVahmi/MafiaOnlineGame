using Framework.Core.ApplicationServices.ApplicationServices;
using Players.ApplicationServices.PlayerAggregate.Dto;

namespace Players.ApplicationServices.PlayerAggregate.Services;

public interface IPlayerApplicationService : IApplicationService
{
    public Task<RegisteredPlayerDto> RegisterAsync(PlayerRegistrationDto playerRegistrationDto, CancellationToken cancellationToken = default);

    public Task ChangeProfileAsync(PlayerChangeProfileDto playerChangeProfileDto, CancellationToken cancellationToken = default);

    public Task<PlayerDto> ViewAsync(long playerId, string userId, CancellationToken cancellationToken = default);
}
