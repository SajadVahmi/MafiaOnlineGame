using Framework.Core.Domian.Aggregates;
using System.Linq.Expressions;
using Framework.Core.Domian.Entities;

namespace Framework.Core.Domian.Data
{
    public interface IRepository
    {
    }
    public interface IRepository<TId, TAggregateRoot> : IRepository where TAggregateRoot : AggregateRoot<TId> where TId : notnull
    {
        Task<TId> GetNextIdAsync(CancellationToken cancellationToken=default);
        void Create(TAggregateRoot aggregate);
        Task<TAggregateRoot?> LoadAsync(TId key, CancellationToken cancellationToken = default);
        Task<bool> ExistAsync(TId key, CancellationToken cancellationToken = default);
        Task<bool> ExistAsync(Expression<Func<TAggregateRoot, bool>> predicate,CancellationToken cancellationToken=default);
    }
  
}
