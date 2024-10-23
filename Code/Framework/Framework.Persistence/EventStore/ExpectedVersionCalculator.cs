using EventStore.Client;
using Framework.Core.Domain.Aggregates;

namespace Framework.Persistence.EventStore;

public static class ExpectedVersionCalculator
{
    public static StreamRevision GetExpectedVersionOfAggregate(IAggregateRoot aggregateRoot)
    {
        var version = aggregateRoot.Version - aggregateRoot.GetEvents().Count() - 1;
        return version < 0 ? StreamRevision.None : StreamRevision.FromInt64(version);     //because index of event store starts from zero
    }
}