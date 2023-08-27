using Framework.Core.Domian.Entities;
using Framework.Core.Domian.Events;
using System.Reflection;

namespace Framework.Core.Domian.Aggregates;

public abstract class AggregateRoot<TId> : Entity<TId>, IAggregateRoot where TId : notnull

{

    private readonly List<IDomainEvent> _events = new();

    protected AggregateRoot() { }


    protected AggregateRoot(IEnumerable<IDomainEvent>? events)
    {
        if (events == null)
            return;

        foreach (var @event in events)
            Mutate(@event);

    }

    protected void Apply(IDomainEvent @event)
    {
        Mutate(@event);

        AddEvent(@event);
    }

    private void Mutate(IDomainEvent @event)
    {
        var onMethod = this.GetType().GetMethod("On", BindingFlags.Instance | BindingFlags.NonPublic, new Type[] { @event.GetType() });

        onMethod?.Invoke(this, new[] { @event });
    }


    protected void AddEvent(IDomainEvent @event) => _events.Add(@event);


    public IEnumerable<IDomainEvent> GetEvents() => _events.AsEnumerable();


    public void ClearEvents() => _events.Clear();
}



