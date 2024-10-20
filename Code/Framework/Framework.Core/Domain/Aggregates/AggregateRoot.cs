using System.Reflection;
using Framework.Core.Domain.Entities;
using Framework.Core.Domain.Events;
using Framework.Core.Domain.Snapshots;

namespace Framework.Core.Domain.Aggregates;

public abstract class AggregateRoot<TId> : Entity<TId>, IAggregateRoot where TId : notnull
{
    private readonly List<IDomainEvent> _events = [];

    protected AggregateRoot() { }


    protected AggregateRoot(IEnumerable<IDomainEvent>? events)
    {
        if (events == null)
            return;

        foreach (var @event in events)
            Apply(@event);

    }

    public long Version { get; protected set; }

    protected void Causes(IDomainEvent @event)
    {
        Apply(@event);

        AddEvent(@event);
    }

    protected void AddEvent(IDomainEvent @event) => _events.Add(@event);

    public IEnumerable<IDomainEvent> GetEvents() => _events.AsEnumerable();

    public void ClearEvents() => _events.Clear();

    public void Apply(IDomainEvent @event)
    {

        var whenMethod = GetType().GetMethod("When", BindingFlags.Instance | BindingFlags.NonPublic, new Type[] { @event.GetType() });

        whenMethod?.Invoke(this, [@event]);

        Version++;
    }

    public virtual void Apply(ISnapshot snapshot) { }
}



