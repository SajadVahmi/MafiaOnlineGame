using Framework.Core.Domain.Events;

namespace Framework.Core.Domain.Aggregates;

public interface IAggregateRoot
{
    void ClearEvents();
    IEnumerable<IDomainEvent> GetEvents();
}
