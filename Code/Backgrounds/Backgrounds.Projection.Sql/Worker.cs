using System.Text;
using Backgrounds.Projection.Sql._Shared;
using EventStore.Client;
using Framework.Core.Domain.Events;
using Framework.Persistence.EventStore;
using Games.Domain.PlayerAggregate.DomainEvents;
using Newtonsoft.Json;

namespace Backgrounds.Projection.Sql
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
                if (!@event.OriginalEvent.EventType.StartsWith("$"))
                {
                    Console.WriteLine($"Event Appeared : {@event.OriginalEvent.EventType}");

                    //TODO: consider using domain event factory
                    var type = typeResolver.GetType(@event.OriginalEvent.EventType);
                    if (type== null)
                    {
                        Console.WriteLine($"Type is null");
                        return;
                    }
                    var body = Encoding.UTF8.GetString(@event.OriginalEvent.Data.ToArray());
                    var domainEvent = JsonConvert.DeserializeObject(body, type);
                    if (domainEvent is not null)
                        await eventBus.Publish((dynamic)domainEvent);      //In-Memory

                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex.ToString());
                throw;
            }
        }
    }
}
