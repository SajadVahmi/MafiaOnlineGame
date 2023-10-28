using Framework.Persistence.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Players.Persistence.SQL.DbContexts;

public class PlayersDbContextFactory : IDesignTimeDbContextFactory<PlayersDbContext>
{
    public required IConfiguration Configuration;

    public PlayersDbContextFactory()
    {
        Configuration =
          new ConfigurationBuilder()
              .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
              .AddJsonFile($"appsettings.Development.json", optional: false, reloadOnChange: true)
              .Build();
    }


    public PlayersDbContext CreateDbContext(string[] args)
    {

        var playersDbContextConfiguration = Configuration.GetSection("Persistence:PlayersDbContext").Get<FrameworkDbContextOptions>();

        if (playersDbContextConfiguration is null)
            throw new Exception("There are not any dbcontext options in configuration.");

        var builder =
            new DbContextOptionsBuilder<PlayersDbContext>()
                .UseSqlServer(playersDbContextConfiguration.ConnectionString);

        return new PlayersDbContext(builder.Options,playersDbContextConfiguration.SaveDomainEvents);

    }
}
