using Framework.Core.Domain.Aggregates;
using Framework.Persistence.EF;
using Framework.Presentation.AspNetCore.Constants;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;

namespace Framework.Test.Api.Fixtures;

public class FrameworkWebApplicationFactory<TProgram>
: WebApplicationFactory<TProgram> where TProgram : class
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {

        builder.UseEnvironment(RunningEnvironmentNames.IntegrationTest);

    }

    public void InitialDatabase<TEntity>(TEntity entity) where TEntity : class
    {
        var scope = base.Services.CreateScope();

        var dbContext = scope.ServiceProvider.GetRequiredService<FrameworkDbContext>();

        dbContext.Set<TEntity>().Add(entity);

        dbContext.SaveChanges();

    }

    public void InitialDatabase<TEntity>(List<TEntity> entities) where TEntity : class
    {
        var scope = base.Services.CreateScope();

        var dbContext = scope.ServiceProvider.GetRequiredService<FrameworkDbContext>();

        dbContext.Set<TEntity>().AddRange(entities);

        dbContext.SaveChanges();

    }
    public TEntity? LoadFromDatabase<TEntity,TKey>(TKey key) where TEntity : AggregateRoot<TKey>
        where TKey:notnull
    {
        var scope = base.Services.CreateScope();

        var dbContext = scope.ServiceProvider.GetRequiredService<FrameworkDbContext>();

      return  dbContext.Set<TEntity>().Find(key);

    }
    protected override void Dispose(bool disposing)
    {
        var scope = base.Services.CreateScope();

        var dbContext = scope.ServiceProvider.GetRequiredService<FrameworkDbContext>();

        dbContext.Database.EnsureDeleted();
    }
}
