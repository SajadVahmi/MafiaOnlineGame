using Framework.Core.Domian.DomainServices;

namespace Players.Domain.PlayerAggregate.Services;

public interface IDuplicateRegistrationCheckService:IDomainService
{
    public Task<bool> CheckAsync(string userId, CancellationToken cancellationToken = default);
}
