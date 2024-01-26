using Framework.Core.Domian.Data;

namespace Framework.Persistence.EF;


public class EntityFrameworkUnitOfWork(FrameworkDbContext dbContext) : IUnitOfWork
{
    public Task BeginAsync()
    {
        return dbContext.Database.BeginTransactionAsync();

    }

    public async Task CommitAsync()
    {
        if (dbContext.Database.CurrentTransaction == null)
        {
            throw new NullReferenceException("Please call `BeginTransaction()` method first.");
        }
        await dbContext.Database.CurrentTransaction.CommitAsync();
        await dbContext.SaveChangesAsync();
    }

    public Task RollbackAsync()
    {
        return dbContext.Database.RollbackTransactionAsync();
    }
}