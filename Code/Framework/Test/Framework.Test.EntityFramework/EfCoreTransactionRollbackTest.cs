using Microsoft.EntityFrameworkCore;
using System.Transactions;

namespace Framework.Test.EntityFramework;

public class EfCoreTransactionRollbackTest<TDbContext>(DbContextOptions dbContextOptions) : IDisposable
    where TDbContext : DbContext
{
    private readonly TransactionScope _scope = new();

    protected TDbContext DbContext = DbContextFactory<TDbContext>.Create(dbContextOptions);

    public void Dispose()
    {
        DbContext.Dispose();

        _scope.Dispose();
    }
}
