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

public class PlayersModule : IFrameworkModule
{
    private readonly IConfiguration _configuration;

    private readonly IServiceCollection _services;

    public PlayersModule(IConfiguration configuration, IServiceCollection services)
    {
        this._configuration = configuration;

        this._services = services;
    }

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
        var playerDbContextOptions = _configuration.GetSection("Persistence:PlayersDbContext").Get<FrameworkDbContextOptions>();

        if (playerDbContextOptions is null)
            throw new Exception("There are not any dbcontext options in configuration.");

        var options =
            new DbContextOptionsBuilder<FrameworkDbContext>()
                .UseSqlServer(playerDbContextOptions.ConnectionString)
                .UseApplicationServiceProvider(_services.BuildServiceProvider())
                .Options;

        var dbContext = new PlayersDbContext(options);

        dbContext.Database.EnsureCreated();

        return dbContext;
    }
}
