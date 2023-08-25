using Framework.Core.Domian.Events;

namespace Framework.Core.Domian.Aggregates;

public interface IAggregateRoot
{
    void ClearEvents();
    IEnumerable<IDomainEvent> GetEvents();
}
