using Framework.Core.Domain.Aggregates;
using Framework.Core.Domain.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Framework.Persistence.EF;

public abstract class EntityFrameworkRepository<TId, TAggregateRoot>(
    FrameworkDbContext commandDbContext,
    IEntityFrameworkSequenceService entityFrameworkSequenceService)
    : IRepository<TId, TAggregateRoot>
    where TAggregateRoot : AggregateRoot<TId>
    where TId : notnull
{
    protected readonly FrameworkDbContext DbContext = commandDbContext;

    protected IEntityFrameworkSequenceService Sequence { get; private set; } = entityFrameworkSequenceService;

    public abstract Task<TId> GetNextIdAsync(CancellationToken cancellationToken = default);

    public Task CreateAsync(TAggregateRoot aggregateRoot, CancellationToken cancellationToken = default)
    {
        DbContext.Set<TAggregateRoot>().Add(aggregateRoot);

        return DbContext.SaveChangesAsync(cancellationToken);
    }

    public Task<TAggregateRoot?> LoadAsync(TId key, CancellationToken cancellationToken = default)
    {
        return DbContext.Set<TAggregateRoot>().FirstOrDefaultAsync(a => a.Id.Equals(key), cancellationToken);
    }

    public Task<bool> ExistAsync(TId key, CancellationToken cancellationToken = default)
    {
        return DbContext.Set<TAggregateRoot>().AnyAsync(a => a.Id.Equals(key), cancellationToken);
    }

    public Task<bool> ExistAsync(Expression<Func<TAggregateRoot, bool>> predicate, CancellationToken cancellationToken = default)
    {
        return DbContext.Set<TAggregateRoot>().AnyAsync(predicate, cancellationToken);
    }

    public Task SaveChangesAsync()
    {
        return DbContext.SaveChangesAsync();
    }

    public void SaveChanges()
    {
        DbContext.SaveChanges();
    }
}
