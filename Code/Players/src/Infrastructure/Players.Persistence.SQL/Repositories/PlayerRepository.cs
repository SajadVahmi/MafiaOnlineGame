using Framework.Persistence.EF;
using Microsoft.EntityFrameworkCore;
using Players.Domain.PlayerAggregate.Data;
using Players.Domain.PlayerAggregate.Models;
using Players.Persistence.SQL.Constants;

namespace Players.Persistence.SQL.Repositories;

public class PlayerRepository(
    FrameworkDbContext commandDbContext,
    IEntityFrameworkSequenceService entityFrameworkSequenceService)
    : EntityFrameworkRepository<PlayerId, Player>(commandDbContext, entityFrameworkSequenceService), IPlayerRepository
{
    public override async Task<PlayerId> GetNextIdAsync(CancellationToken cancellationToken = default)
    {
        var id = await Sequence.Next(Names.PlayersSequence);

        if (id is null)
            throw new Exception("The fetched id value is null");

        return PlayerId.Instantiate(long.Parse(id));
    }

    public Task<Player?> LoadAsync(PlayerId playerId, string userId, CancellationToken cancellationToken = default)
    {
        return DbContext.Set<Player>().FirstOrDefaultAsync(player => player.Id == playerId && player.UserId == userId, cancellationToken);
    }

    public Task<Player?> ViewAsync(PlayerId playerId, string userId, CancellationToken cancellationToken = default)
    {
        return DbContext.Set<Player>().FirstOrDefaultAsync(player => player.Id == playerId && player.UserId == userId, cancellationToken);
    }

    public Task RegisterAsync(Player player, CancellationToken cancellationToken = default)
    {
        DbContext.Add(player);

        return DbContext.SaveChangesAsync(cancellationToken);
    }


}
