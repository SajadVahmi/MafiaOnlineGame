using System.Linq.Expressions;
using Framework.Core.Domian.Aggregates;
using Framework.Core.Domian.Data;
using Microsoft.EntityFrameworkCore;

namespace Framework.EntityFramework.Commands;

public abstract class EfRepository<TId, TAggregateRoot> : IRepository<TId, TAggregateRoot> 
    where TAggregateRoot : AggregateRoot<TId> where TId : notnull
{
    protected readonly CommandDbContext DbContext;

    protected EfRepository(CommandDbContext commandDbContext)
    {
        DbContext = commandDbContext;
    }


    public abstract Task<TId> GetNextIdAsync(CancellationToken cancellationToken = default);

    public void Create(TAggregateRoot aggregate)
    {
        DbContext.Set<TAggregateRoot>().Add(aggregate);
    }

    public Task<TAggregateRoot?> LoadAsync(TId key, CancellationToken cancellationToken = default)
    {
       return DbContext.Set<TAggregateRoot>().FirstOrDefaultAsync(a=>a.Id.Equals(key),cancellationToken);
    }

    public Task<bool> ExistAsync(TId key, CancellationToken cancellationToken = default)
    {
        return DbContext.Set<TAggregateRoot>().AnyAsync(a => a.Id.Equals(key), cancellationToken);
    }

    public Task<bool> ExistAsync(Expression<Func<TAggregateRoot, bool>> predicate, CancellationToken cancellationToken = default)
    {
        return DbContext.Set<TAggregateRoot>().AnyAsync(predicate, cancellationToken);
    }
}