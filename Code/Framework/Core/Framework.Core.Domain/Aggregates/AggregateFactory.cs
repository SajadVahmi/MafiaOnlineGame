using Framework.Core.Domain.Events;
using Framework.Core.Domain.Snapshots;

namespace Framework.Core.Domain.Aggregates;

public class AggregateFactory : IAggregateFactory
{
    public T Create<T>(List<IDomainEvent> events, ISnapshot snapshot) where T : IAggregateRoot
    {
        var aggregate = (T)Activator.CreateInstance(typeof(T),true)!;

        if (snapshot != EmptySnapshot.Instance) 
            aggregate.Apply(snapshot);

        foreach (var domainEvent in events)
        {
            aggregate.Apply(domainEvent);
        }
        return aggregate;
    }

}