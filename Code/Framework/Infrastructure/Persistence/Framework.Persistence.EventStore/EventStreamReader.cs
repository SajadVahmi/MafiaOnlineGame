using EventStore.Client;

namespace Framework.Persistence.EventStore;

internal static class EventStreamReader
{
    public static ValueTask<List<ResolvedEvent>> Read(EventStoreClient client, string streamId, long start, long end=int.MaxValue)
    {
        var streamEvents = client.ReadStreamAsync(
            Direction.Forwards,
            streamId,
            StreamPosition.Start

        );
        return streamEvents.ToListAsync();

    }
}