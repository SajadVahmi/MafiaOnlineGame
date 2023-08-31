using Framework.Core.Domian.Aggregates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Framework.Infra.EF.Commands;
public static class ChangeTrackerExtensions
{
    public static List<IAggregateRoot> GetChangedAggregates(this ChangeTracker changeTracker) =>
   changeTracker.Entries<IAggregateRoot>()
                            .Where(x => x.State == EntityState.Modified ||
                                   x.State == EntityState.Added ||
                                   x.State == EntityState.Deleted).Select(c => c.Entity).ToList();

    public static List<IAggregateRoot> GetAggregatesWithEvent(this ChangeTracker changeTracker) =>
            changeTracker.Entries<IAggregateRoot>()
                                     .Where(x => x.State != EntityState.Detached).Select(c => c.Entity).Where(c => c.GetEvents().Any()).ToList();

}
