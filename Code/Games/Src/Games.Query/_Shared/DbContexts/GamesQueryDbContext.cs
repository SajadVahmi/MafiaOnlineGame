using Games.Domain.PlayerAggregate.Models;
using Games.Query.PlayerAggregate.Configurations;
using Games.Query.PlayerAggregate.Models;
using Microsoft.EntityFrameworkCore;

namespace Games.Query._Shared.DbContexts;

public sealed class GamesQueryDbContext:DbContext
{
    public GamesQueryDbContext(DbContextOptions options)
        : base(options)
    {
        ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(PlayerEntityTypeConfiguration).Assembly);
        base.OnModelCreating(modelBuilder);
    }
    public DbSet<PlayerQueryModel> Players { get; set; }

    public override int SaveChanges()
    {
        throw new Exception("the query db context cannot save any entity...!");

    }
    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        throw new Exception("the query db context cannot save any entity...!");

    }

}