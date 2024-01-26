using Framework.Core.Contracts;
using Framework.Events.OutBox.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;

namespace Framework.Persistence.EF;

public class FrameworkDbContext : DbContext
{
    private IDbContextTransaction? _transaction;

    protected FrameworkDbContext() { }

    public FrameworkDbContext(DbContextOptions options, bool saveDomainEvents = false) : base(options)
    {
        SaveDomainEvents = saveDomainEvents;
    }

    protected bool SaveDomainEvents { get; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        if (SaveDomainEvents)

            modelBuilder.ApplyConfiguration(new OutBoxEventItemConfiguration());

        else

            modelBuilder.Ignore<OutBoxEventItem>();

        base.OnModelCreating(modelBuilder);
    }

    protected override void ConfigureConventions(ModelConfigurationBuilder builder)
    {
        base.ConfigureConventions(builder);

        builder.Properties<DateOnly>()
            .HaveConversion<DateOnlyConverter>();

        builder.Properties<TimeOnly>()
            .HaveConversion<TimeOnlyConverter>();
    }

    public void BeginTransaction()
    {
        _transaction = Database.BeginTransaction();
    }

    public void RollbackTransaction()
    {
        if (_transaction == null)
        {
            throw new NullReferenceException("Please call `BeginTransaction()` method first.");
        }

        _transaction.Rollback();
    }

    public void CommitTransaction()
    {
        if (_transaction == null)
        {
            throw new NullReferenceException("Please call `BeginTransaction()` method first.");
        }

        _transaction.Commit();
    }

    public override int SaveChanges()
    {
        ChangeTracker.DetectChanges();

        BeforeSaveTriggers();

        ChangeTracker.AutoDetectChangesEnabled = false;

        var result = base.SaveChanges();

        ChangeTracker.AutoDetectChangesEnabled = true;

        return result;
    }

    public override Task<int> SaveChangesAsync(
        bool acceptAllChangesOnSuccess,
        CancellationToken cancellationToken = default)
    {
        ChangeTracker.DetectChanges();

        BeforeSaveTriggers();

        ChangeTracker.AutoDetectChangesEnabled = false;

        var result = base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);

        ChangeTracker.AutoDetectChangesEnabled = true;

        return result;
    }


    private void BeforeSaveTriggers()
    {
        if (SaveDomainEvents)

            PersistDomainEventsInOutBoxEventTable();
    }

    private void PersistDomainEventsInOutBoxEventTable()
    {
        var changedAggregates = ChangeTracker.GetAggregatesWithEvent();

        var authenticatedUser = this.GetService<IAuthenticatedUser>();

        var serializer = this.GetService<IJsonSerializerAdapter>();

        var domainEvents = changedAggregates
            .SelectMany(aggregateRoot => OutBoxEventItemFactory.Create(aggregateRoot, aggregateRoot.GetEvents(), serializer, authenticatedUser))
            .ToList();

        Set<OutBoxEventItem>().AddRange(domainEvents);

        changedAggregates.ForEach(a => a.ClearEvents());
    }
}
