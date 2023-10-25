using Framework.Core.Domian.Data;
using Framework.Persistence.EF;
using Players.Domain.PlayerAggregate.Data;
using Players.Domain.PlayerAggregate.Models;
using Players.Persistence.SQL.Constants;

namespace Players.Persistence.SQL.Repositories;

public class PlayerRepository : EntityFrameworkRepository<PlayerId, Player>, IPlayerRepository
{

    public PlayerRepository(FrameworkDbContext commandDbContext) : base(commandDbContext)
    {
    }

    public override async Task<PlayerId> GetNextIdAsync(CancellationToken cancellationToken = default)
    {
        var id = await Sequence.Next(Names.PlayersSequence);

        return  PlayerId.Instantiate(id);
    }

    public async Task RegisterAsync(Player player, CancellationToken cancellationToken = default)
    {
        
    }


}
