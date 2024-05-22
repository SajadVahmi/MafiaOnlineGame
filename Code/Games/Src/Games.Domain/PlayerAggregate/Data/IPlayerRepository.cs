using Framework.Core.Domain.Data;
using Games.Domain.PlayerAggregate.Models;

namespace Games.Domain.PlayerAggregate.Data;

public interface IPlayerRepository:IRepository
{
    Task<Player> GetAsync(PlayerId id,CancellationToken cancellationToken=default);
    Task AddAsync(Player player, CancellationToken cancellationToken = default);
    Task UpdateAsync(Player player, CancellationToken cancellationToken = default);
}