using Framework.Core.Domian.Data;
using Players.Domain.PlayerAggregate.Models;

namespace Players.Domain.PlayerAggregate.Data;

public interface IPlayerRepository:IRepository<PlayerId,Player>
{
    public Task RegisterAsync(Player player,CancellationToken cancellationToken=default);

}
