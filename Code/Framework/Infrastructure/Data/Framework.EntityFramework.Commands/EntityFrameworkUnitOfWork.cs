using Framework.Core.Domian.Data;
using Microsoft.EntityFrameworkCore.Storage;

namespace Framework.EntityFramework.Commands;

public class EfUnitOfWork : IUnitOfWork
{
    private readonly CommandDbContext _dbContext;
    private IDbContextTransaction? _transaction;
    public EfUnitOfWork(CommandDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task BeginAsync(CancellationToken cancellationToken=default)
    {
        this._transaction = await _dbContext.Database.BeginTransactionAsync(cancellationToken);
    }

    public async Task CommitAsync(CancellationToken cancellationToken=default)
    {
        if (_transaction is null ||
            _dbContext.Database.CurrentTransaction is null)
            throw new Exception("Cannot commit on null transaction object");

        await _dbContext.Database.CurrentTransaction.CommitAsync(cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    
    public async Task RollbackAsync(CancellationToken cancellationToken = default)
    {

        if (_transaction is null ||
            _dbContext.Database.CurrentTransaction is null)
            throw new Exception("Cannot rollback on null transaction object");

        await this._transaction.RollbackAsync(cancellationToken);
    }
}