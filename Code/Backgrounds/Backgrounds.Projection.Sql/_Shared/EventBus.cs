using Framework.Core.Domain.Events;

namespace Backgrounds.Projection.Sql._Shared;

public class EventBus(IServiceProvider serviceProvider) : IEventBus
{
    public async Task Publish<T>(T eventToPublish) where T : IEvent
    {
        var handler = serviceProvider.GetService<IEventHandler<T>>();
        if (handler is not null)
            await handler.Handle(eventToPublish);
    }
}