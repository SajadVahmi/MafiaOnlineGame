using Framework.Core.Contracts;
using Framework.Persistence.EF;
using Framework.Presentation.AspNetCore.Constants;
using Framework.Test.Stubs;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;

namespace Players.RestApi.IntegrationTests.V1.PlayerAggregates.Fixtures;

public class PlayersWebApplicationFactory
: WebApplicationFactory<Program>
{

    protected IServiceCollection services;

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {

        builder.UseEnvironment(RunningEnvironmentNames.IntegrationTest);

    }

    protected override void Dispose(bool disposing)
    {
        var scope = base.Services.CreateScope();

        var dbContext = scope.ServiceProvider.GetRequiredService<FrameworkDbContext>();

        dbContext.Database.EnsureDeleted();
    }
}
