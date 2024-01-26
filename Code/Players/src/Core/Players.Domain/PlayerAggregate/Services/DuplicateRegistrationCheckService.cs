using Players.Domain.PlayerAggregate.Data;

namespace Players.Domain.PlayerAggregate.Services;

public class DuplicateRegistrationCheckService(IPlayerRepository playerRepository)
    : IDuplicateRegistrationCheckService
{
    public Task<bool> CheckAsync(string userId, CancellationToken cancellationToken = default)
    {
        return playerRepository.ExistAsync(player => player.UserId == userId, cancellationToken); 
    }
}