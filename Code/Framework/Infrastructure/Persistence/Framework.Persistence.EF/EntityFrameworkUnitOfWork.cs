using Framework.Core.Domian.Data;

namespace Framework.Persistence.EF;


public class EntityFrameworkUnitOfWork : IUnitOfWork
{
    private readonly FrameworkDbContext _dbContext;

    public EntityFrameworkUnitOfWork(FrameworkDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public Task BeginAsync()
    {
        return _dbContext.Database.BeginTransactionAsync();

    }

    public async Task CommitAsync()
    {
        if (_dbContext.Database.CurrentTransaction == null)
        {
            throw new NullReferenceException("Please call `BeginTransaction()` method first.");
        }
        await _dbContext.Database.CurrentTransaction.CommitAsync();
        await _dbContext.SaveChangesAsync();
    }

    public Task RollbackAsync()
    {
        return this._dbContext.Database.RollbackTransactionAsync();
    }
}