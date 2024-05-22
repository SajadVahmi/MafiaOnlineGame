using Framework.Core.Domain.Aggregates;
using Framework.Core.Domain.Data;

namespace Framework.Persistence.EventStore.Repositories
{
    public interface IEventSourceRepository<T, in TKey>: IRepository where T : AggregateRoot<TKey> where TKey : notnull
    {
        Task<T> GetByIdAsync(TKey id,CancellationToken cancellationToken=default);
        Task AppendEventsAsync(T aggregate,CancellationToken cancellationToken=default);
    }
}