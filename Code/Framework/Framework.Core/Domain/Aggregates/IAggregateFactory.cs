using Framework.Core.Domain.Events;
using Framework.Core.Domain.Snapshots;

namespace Framework.Core.Domain.Aggregates;

public interface IAggregateFactory
{
    T Create<T>(List<IDomainEvent> events, ISnapshot snapshot) where T : IAggregateRoot;
}