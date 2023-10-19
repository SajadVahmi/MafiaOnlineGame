using Framework.Core.Contracts;

namespace Framework.Core.Domian.Events;

public abstract class DomainEvent : IDomainEvent
{
    

    public DomainEvent(string id,DateTimeOffset whenItHappened)
    {
        EventId = id;

        WhenItHappened = whenItHappened;
    }

    public string EventId { get; private set; }

    public DateTimeOffset WhenItHappened { get; private set; }
}
