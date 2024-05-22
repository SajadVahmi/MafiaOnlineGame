using Framework.Persistence.EventStore.Repositories;
using Games.Domain.PlayerAggregate.Data;
using Games.Domain.PlayerAggregate.Models;

namespace Games.Persistence.EventStore;

public class PlayerRepository : IPlayerRepository
{
    private readonly IEventSourceRepository<Player, PlayerId> _repository;

    public PlayerRepository(IEventSourceRepository<Player, PlayerId> repository)
    {
        _repository = repository;
    }
    public Task AddAsync(Player player, CancellationToken cancellationToken = default)
    {
        return _repository.AppendEventsAsync(player,cancellationToken);
    }

    public Task<Player> GetAsync(PlayerId id, CancellationToken cancellationToken = default)
    {
        return _repository.GetByIdAsync(id, cancellationToken);
    }

    public Task UpdateAsync(Player player, CancellationToken cancellationToken = default)
    {
        return _repository.AppendEventsAsync(player, cancellationToken);
    }
}