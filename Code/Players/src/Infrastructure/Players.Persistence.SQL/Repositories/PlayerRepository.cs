using Framework.Persistence.EF;
using Players.Domain.PlayerAggregate.Data;
using Players.Domain.PlayerAggregate.Models;
using Players.Persistence.SQL.Constants;
using Players.Persistence.SQL.DbContexts;

namespace Players.Persistence.SQL.Repositories;

public class PlayerRepository : EntityFrameworkRepository<PlayerId, Player>, IPlayerRepository
{

    public PlayerRepository(FrameworkDbContext commandDbContext, IEntityFrameworkSequenceService entityFrameworkSequenceService) : base(commandDbContext, entityFrameworkSequenceService)
    {
    }

    public override async Task<PlayerId> GetNextIdAsync(CancellationToken cancellationToken = default)
    {
        var id = await Sequence.Next(Names.PlayersSequence);

        if (id is null)
            throw new Exception("The fetched id value is null");

        return PlayerId.Instantiate(long.Parse(id));
    }

    public async Task RegisterAsync(Player player, CancellationToken cancellationToken = default)
    {
        DbContext.Add(player);

        await DbContext.SaveChangesAsync(cancellationToken);
    }


}
