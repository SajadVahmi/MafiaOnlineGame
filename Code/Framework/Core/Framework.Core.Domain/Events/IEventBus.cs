namespace Framework.Core.Domain.Events;

public interface IEventBus
{
    Task Publish<T>(T eventToPublish) where T : IEvent;
}