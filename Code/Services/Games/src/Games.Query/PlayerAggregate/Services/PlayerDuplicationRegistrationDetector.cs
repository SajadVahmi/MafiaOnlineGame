using Games.Domain.PlayerAggregate.Contracts;
using Games.Query._Shared.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace Games.Query.PlayerAggregate.Services;

public class PlayerDuplicationRegistrationDetector(GamesQueryDbContext dbContext): IPlayerDuplicationRegistrationDetector
{
    public Task<bool> DuplicateRegistrationIsGoingToHappenAsync(string userId, CancellationToken cancellationToken=default)
    {
        return dbContext.Players.AnyAsync(player => player.UserId == userId,cancellationToken);
    }
}