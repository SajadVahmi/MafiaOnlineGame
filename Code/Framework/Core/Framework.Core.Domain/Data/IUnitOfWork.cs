namespace Framework.Core.Domain.Data;

public interface IUnitOfWork
{

    Task BeginAsync();

    Task CommitAsync();

    Task RollbackAsync();

}