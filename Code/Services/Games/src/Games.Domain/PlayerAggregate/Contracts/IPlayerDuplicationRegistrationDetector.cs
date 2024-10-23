using Framework.Core.Domain.DomainServices;

namespace Games.Domain.PlayerAggregate.Contracts;

public interface IPlayerDuplicationRegistrationDetector:IDomainService
{
    public Task<bool> DuplicateRegistrationIsGoingToHappenAsync(string userId, CancellationToken cancellationToken=default);
}