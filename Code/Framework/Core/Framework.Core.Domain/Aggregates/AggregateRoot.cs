using System.Reflection;
using Framework.Core.Domain.Entities;
using Framework.Core.Domain.Events;

namespace Framework.Core.Domain.Aggregates;

public abstract class AggregateRoot<TId> : Entity<TId>, IAggregateRoot where TId : notnull

{

    private readonly List<IDomainEvent> _events = new();

    protected AggregateRoot() { }


    protected AggregateRoot(IEnumerable<IDomainEvent>? events)
    {
        if (events == null)
            return;

        foreach (var @event in events)
            Apply(@event);

    }

    protected void Causes(IDomainEvent @event)
    {
        Apply(@event);

        AddEvent(@event);
    }

    private void Apply(IDomainEvent @event)
    {

        var whenMethod = this.GetType().GetMethod("When", BindingFlags.Instance | BindingFlags.NonPublic, new Type[] { @event.GetType() });

        whenMethod?.Invoke(this, new object?[] { @event });
    }


    protected void AddEvent(IDomainEvent @event) => _events.Add(@event);


    public IEnumerable<IDomainEvent> GetEvents() => _events.AsEnumerable();


    public void ClearEvents() => _events.Clear();
}



