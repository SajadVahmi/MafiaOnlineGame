using Framework.Core.ApplicationServices.ApplicationServices;
using Players.ApplicationServices.PlayerAggregate.Dtos;

namespace Players.ApplicationServices.PlayerAggregate.Services;

public interface IPlayerApplicationService : IApplicationService
{
    public Task<RegisteredPlayerDto> RegisterAsync(PlayerRegistrationDto playerRegistrationDto, CancellationToken cancellationToken = default);
}
