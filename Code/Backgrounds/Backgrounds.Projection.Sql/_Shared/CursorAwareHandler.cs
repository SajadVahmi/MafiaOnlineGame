using EventStore.Client;
using Framework.Core.Domain.Events;

namespace Backgrounds.Projection.Sql._Shared;

public class CursorAwareHandler<T>(IEventHandler<T> handler, ICursor cursor) : IEventHandler<T>
    where T : IEvent
{
    public async Task Handle(T @event)
    {

        await handler.Handle(@event);
        var newPosition = cursor.CurrentPosition().CommitPosition;

        if (true)
        {
            
        }

    }
}