using Framework.Core.Contracts;
using Framework.Core.Domain.Aggregates;

namespace Framework.Core.Domain.Events;

public interface IEventStore
{
    Task<List<IDomainEvent>> GetEventsOfStreamAsync<T, TKey>(TKey id,IJsonSerializerAdapter jsonSerializer,CancellationToken cancellationToken=default) where T : AggregateRoot<TKey> where TKey : notnull;
    Task<List<IDomainEvent>> GetEventsOfStreamAsync<T, TKey>(TKey id, int fromIndex, IJsonSerializerAdapter jsonSerializer,CancellationToken cancellationToken=default) where T : AggregateRoot<TKey> where TKey : notnull;
    Task AppendEventsAsync<T,TKey>(T aggregateRoot,IJsonSerializerAdapter jsonSerializer,CancellationToken cancellationToken=default) where T : AggregateRoot<TKey> where TKey : notnull;
}