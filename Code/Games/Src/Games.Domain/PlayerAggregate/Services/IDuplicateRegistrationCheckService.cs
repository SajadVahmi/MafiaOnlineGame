using Framework.Core.Domain.DomainServices;
using Games.Domain.PlayerAggregate.Models;

namespace Games.Domain.PlayerAggregate.Services;

public interface IDuplicateRegistrationCheckService : IDomainService
{
    public Task<bool> CheckAsync(PlayerUserId userId, CancellationToken cancellationToken = default);
}