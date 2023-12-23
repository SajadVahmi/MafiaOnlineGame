using Microsoft.EntityFrameworkCore;
using System.Transactions;

namespace Framework.Test.EntityFramework;

public class EfCoreTransactionRollbackTest<TDbContext> : IDisposable where TDbContext : DbContext
{
    private TransactionScope _scope;

    protected TDbContext DbContext;

    public EfCoreTransactionRollbackTest(DbContextOptions dbContextOptions)
    {
        _scope = new TransactionScope();

        DbContext = DbContextFactory<TDbContext>.Create(dbContextOptions);
    }

    public void Dispose()
    {
        DbContext.Dispose();

        _scope.Dispose();
    }
}
