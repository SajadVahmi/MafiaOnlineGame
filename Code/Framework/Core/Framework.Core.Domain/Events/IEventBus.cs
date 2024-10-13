namespace Framework.Core.Domain.Events;

public interface IEventBus
{
    Task PublishAsync<T>(T eventToPublish,CancellationToken cancellationToken=default) where T : IEvent;
}