using Microsoft.EntityFrameworkCore;

namespace Framework.EntityFramework.Queries;

public class QueryDbContext : DbContext
{
    public QueryDbContext(DbContextOptions options)
        : base(options)
    {
        this.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
    }

    public override int SaveChanges()
    {
        throw new Exception("the query db context cannot save any entity...!");

    }
    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        throw new Exception("the query db context cannot save any entity...!");

    }
}