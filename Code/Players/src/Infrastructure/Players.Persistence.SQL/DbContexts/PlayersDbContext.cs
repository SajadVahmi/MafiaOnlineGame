using Framework.Persistence.EF;
using Microsoft.EntityFrameworkCore;
using Players.Domain.PlayerAggregate.Models;
using Players.Persistence.SQL.Configurations;
using Players.Persistence.SQL.Constants;

namespace Players.Persistence.SQL.DbContexts;

public class PlayersDbContext : FrameworkDbContext
{
    public PlayersDbContext(DbContextOptions options) : base(options, true) { }

    public virtual DbSet<Player> Players { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(PlayerConfiguration).Assembly);

        CreateSequence(modelBuilder);
    }

    
    private void CreateSequence(ModelBuilder builder)
    {
        builder.HasSequence<long>(Names.PlayersSequence)
           .StartsAt(1)
           .IncrementsBy(1)
           .HasMin(1);
    }

}
