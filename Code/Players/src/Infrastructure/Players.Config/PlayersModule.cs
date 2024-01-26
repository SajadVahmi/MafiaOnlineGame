using Framework.Configuration;
using Framework.Persistence.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Players.ApplicationServices.PlayerAggregate.Services;
using Players.Domain.PlayerAggregate.Services;
using Players.Persistence.SQL.DbContexts;
using Players.Persistence.SQL.Repositories;

namespace Players.Config;

public class PlayersModule(IConfiguration configuration, IServiceCollection services) : IFrameworkModule
{
    public void Register(IDependencyRegister dependencyRegister)
    {
        dependencyRegister.RegisterDomainServices(typeof(DuplicateRegistrationCheckService).Assembly);

        dependencyRegister.RegisterApplicationServices(typeof(PlayerApplicationService).Assembly);

        dependencyRegister.RegisterRepositories(typeof(PlayerRepository).Assembly);

        dependencyRegister.RegisterScoped(CreateDbContext);

        dependencyRegister.RegisterScoped<IEntityFrameworkSequenceService,EntityFrameworkSequenceService>();
    }

    private FrameworkDbContext CreateDbContext()
    {
        var playerDbContextOptions = configuration.GetSection("Persistence:PlayersDbContext").Get<FrameworkDbContextOptions>();

        if (playerDbContextOptions is null)
            throw new Exception("There are not any db context options in configuration.");

        var options =
            new DbContextOptionsBuilder<FrameworkDbContext>()
                .UseSqlServer(playerDbContextOptions.ConnectionString)
                .UseApplicationServiceProvider(services.BuildServiceProvider())
                .Options;

        var dbContext = new PlayersDbContext(options);

        dbContext.Database.EnsureCreated();

        return dbContext;
    }
}
