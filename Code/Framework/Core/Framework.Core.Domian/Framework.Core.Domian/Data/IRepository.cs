using Framework.Core.Domian.Aggregates;
using System.Linq.Expressions;

namespace Framework.Core.Domian.Data
{
    public interface IRepository
    {
    }
    public interface IRepository<TKey, T> : IRepository where T : IAggregateRoot
    {
        Task<TKey> GetNextId();
        Task Create(T aggregate);
        Task Remove(T aggregate);
        Task<T> Get(TKey key);
        Task<T> Get(Expression<Func<T, bool>> predicate);
    }

}
