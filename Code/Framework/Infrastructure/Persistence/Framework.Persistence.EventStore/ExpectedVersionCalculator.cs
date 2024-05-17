using Framework.Core.Domain.Aggregates;

namespace Framework.Persistence.EventStore;

public static class ExpectedVersionCalculator
{
    public static int GetExpectedVersionOfAggregate(IAggregateRoot aggregateRoot)
    {
        var version = aggregateRoot.Version - aggregateRoot.GetEvents().Count();
        return version - 1;     //because index of event store starts from zero
    }
}