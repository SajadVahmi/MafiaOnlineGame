using System.Text;
using EventStore.Client;
using Framework.Core.Domain.Events;
using Framework.Persistence.EventStore;
using Framework.Projection;
using Games.Contract.DomainEvents;
using Newtonsoft.Json;

namespace Backgrounds.SqlServerProjection
{
    public class Worker(
        ILogger<Worker> logger,
        ICursor cursor,
        IEventTypeResolver typeResolver,
        IEventBus eventBus, EventStoreClient eventStoreClient) : BackgroundService
    {

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            typeResolver.AddTypesFromAssembly(typeof(PlayerRegistered).Assembly);

            await using var subscription = eventStoreClient.SubscribeToAll(
                FromAll.After(cursor.CurrentPosition()),
                cancellationToken: cancellationToken);

            await foreach (var message in subscription.Messages.WithCancellation(cancellationToken))
            {

                switch (message)
                {
                    case StreamMessage.Event(var @event):
                        Console.WriteLine($@"Received event {@event.OriginalEventNumber}@{@event.OriginalStreamId}");
                        await HandleEvent(@event);
                        break;
                }

            }
        }

        private async Task HandleEvent(ResolvedEvent @event)
        {
            try
            {

                if (@event.OriginalEvent.EventType.StartsWith("$"))
                    return;

                cursor.MoveTo(@event.OriginalPosition!.Value);

                Console.WriteLine($"Event Appeared : {@event.OriginalEvent.EventType}");

                //TODO: consider using domain event factory
                var type = typeResolver.GetType(@event.OriginalEvent.EventType);

                if (type == null)
                {
                    Console.WriteLine($"Type is null");
                    return;
                }
                var body = Encoding.UTF8.GetString(@event.OriginalEvent.Data.ToArray());
                var domainEvent = JsonConvert.DeserializeObject(body, type);
                if (domainEvent is not null)
                    await eventBus.PublishAsync((dynamic)domainEvent);      //In-Memory

            }
            catch (Exception ex)
            {
                logger.LogError(ex.ToString());
                throw;
            }
        }
    }
}
