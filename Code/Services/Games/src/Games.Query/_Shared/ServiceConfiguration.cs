using Games.Query._Shared.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Games.Query._Shared;

public static class ServiceConfiguration
{
    public static IServiceCollection AddQueryDbContext(this IServiceCollection services,string connectionString)
    {
        services.AddDbContext<GamesQueryDbContext>(options =>
            options.UseSqlServer(connectionString));

        return services;
    }
}