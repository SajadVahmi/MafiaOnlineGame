using Framework.Core.Contracts;

namespace Framework.Core.Domian.Events;

public abstract class DomainEvent : IDomainEvent
{
    public DomainEvent(IEventIdProvider idProvider, IClock clock)
    {
        EventId = idProvider.Get();

        WhenItHappened = clock.Now();
    }

    public string EventId { get; private set; }

    public DateTimeOffset WhenItHappened { get; private set; }
}
