namespace Framework.Core.Domian.Data;

public interface IUnitOfWork
{

    Task BeginAsync();

    Task CommitAsync();

    Task RollbackAsync();

}