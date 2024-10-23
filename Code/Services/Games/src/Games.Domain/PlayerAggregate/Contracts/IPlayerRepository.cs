using Framework.Core.Domain.Repository;
using Framework.Core.Domain.ValueObjects;
using Games.Domain.PlayerAggregate.Models;

namespace Games.Domain.PlayerAggregate.Contracts;

public interface IPlayerRepository:IRepository
{
    Task<Player> GetAsync(EntityId id,CancellationToken cancellationToken=default);
    Task AddAsync(Player player, CancellationToken cancellationToken = default);
    Task UpdateAsync(Player player, CancellationToken cancellationToken = default);
}