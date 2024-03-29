﻿using System.Linq.Expressions;
using Framework.Core.Domain.Aggregates;

namespace Framework.Core.Domain.Data;

public interface IRepository;
public interface IRepository<TId, TAggregateRoot> : IRepository where TAggregateRoot : AggregateRoot<TId> where TId : notnull
{
    Task CreateAsync(TAggregateRoot aggregateRoot, CancellationToken cancellationToken = default);

    Task<TId> GetNextIdAsync(CancellationToken cancellationToken = default);

    Task<TAggregateRoot?> LoadAsync(TId key, CancellationToken cancellationToken = default);

    Task<bool> ExistAsync(Expression<Func<TAggregateRoot, bool>> predicate, CancellationToken cancellationToken = default);

    Task SaveChangesAsync();
    void SaveChanges();

}
