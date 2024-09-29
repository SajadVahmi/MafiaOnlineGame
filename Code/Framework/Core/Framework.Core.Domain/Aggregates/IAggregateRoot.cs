using Framework.Core.Domain.Events;
using Framework.Core.Domain.Snapshots;

namespace Framework.Core.Domain.Aggregates;

public interface IAggregateRoot
{
    long Version { get; }
    void ClearEvents();
    IEnumerable<IDomainEvent> GetEvents();
    void Apply(IDomainEvent @event);
    void Apply(ISnapshot snapshot);
}
