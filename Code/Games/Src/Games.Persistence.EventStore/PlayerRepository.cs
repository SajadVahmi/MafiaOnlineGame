using Framework.Persistence.EventStore.Repositories;
using Games.Domain.PlayerAggregate.Data;
using Games.Domain.PlayerAggregate.Models;

namespace Games.Persistence.EventStore;

public class PlayerRepository(IEventSourceRepository<Player, PlayerId> repository) : IPlayerRepository
{
    public Task AddAsync(Player player, CancellationToken cancellationToken = default)
    {
        return repository.AppendEventsAsync(player,cancellationToken);
    }

    public Task<Player> GetAsync(PlayerId id, CancellationToken cancellationToken = default)
    {
        return repository.GetByIdAsync(id, cancellationToken);
    }

    public Task UpdateAsync(Player player, CancellationToken cancellationToken = default)
    {
        return repository.AppendEventsAsync(player, cancellationToken);
    }
}