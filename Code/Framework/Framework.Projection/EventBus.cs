using Framework.Core.Domain.Events;
using Microsoft.Extensions.DependencyInjection;

namespace Framework.Projection;

public class EventBus(IServiceProvider serviceProvider) : IEventBus
{
    public async Task PublishAsync<T>(T eventToPublish,CancellationToken cancellationToken=default) where T : IEvent
    {
        var handler = serviceProvider.GetService<IEventHandler<T>>();
        if (handler is not null)
            await handler.HandleAsync(eventToPublish,cancellationToken);
    }
}