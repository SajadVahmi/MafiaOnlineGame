namespace Framework.Core.Domain.Events;

public interface IEventHandler<in T> where T : IEvent
{
    Task Handle(T @event);
}