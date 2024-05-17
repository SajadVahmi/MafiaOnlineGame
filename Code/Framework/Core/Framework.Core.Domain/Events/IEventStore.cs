using Framework.Core.Contracts;
using Framework.Core.Domain.Aggregates;

namespace Framework.Core.Domain.Events;

public interface IEventStore
{
    Task<List<IDomainEvent>> GetEventsOfStream<T, TKey>(TKey id,IJsonSerializerAdapter jsonSerializer) where T : AggregateRoot<TKey>;
    Task<List<IDomainEvent>> GetEventsOfStream<T, TKey>(TKey id, int fromIndex, IJsonSerializerAdapter jsonSerializer) where T : AggregateRoot<TKey>;
    Task AppendEvents<T,TKey>(T aggregateRoot,IJsonSerializerAdapter jsonSerializer) where T : AggregateRoot<TKey>;
}