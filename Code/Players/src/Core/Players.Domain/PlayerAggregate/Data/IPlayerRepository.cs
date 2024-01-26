using Framework.Core.Domian.Data;
using Players.Domain.PlayerAggregate.Models;

namespace Players.Domain.PlayerAggregate.Data;

public interface IPlayerRepository : IRepository<PlayerId, Player>
{
    public void Register(Player player);

    public Task<Player?> LoadAsync(PlayerId playerId, string userId, CancellationToken cancellationToken = default);

    public Task<Player?> ViewAsync(PlayerId playerId, string userId, CancellationToken cancellationToken = default);
}
