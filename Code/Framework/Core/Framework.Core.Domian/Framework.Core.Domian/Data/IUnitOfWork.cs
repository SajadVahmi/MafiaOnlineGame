namespace Framework.Core.Domian.Data;

public interface IUnitOfWork
{
    Task BeginAsync(CancellationToken cancellationToken = default);

    Task CommitAsync(CancellationToken cancellationToken = default);

    Task RollbackAsync(CancellationToken cancellationToken = default);

}