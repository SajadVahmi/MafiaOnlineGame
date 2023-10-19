namespace Players.Domain.PlayerAggregate.Services;

public interface IDuplicateRegistrationCheckService
{
    public Task<bool> CheckAsync(string userId, CancellationToken cancellationToken = default);
}
