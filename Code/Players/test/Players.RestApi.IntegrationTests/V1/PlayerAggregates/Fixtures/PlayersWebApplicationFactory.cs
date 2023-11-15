using Framework.Core.Contracts;
using Framework.Persistence.EF;
using Framework.Presentation.AspNetCore.Constants;
using Framework.Test.Stubs;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Players.Persistence.SQL.DbContexts;

namespace Players.RestApi.IntegrationTests.V1.PlayerAggregates.Fixtures;

public class PlayersWebApplicationFactory
: WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {

        builder.UseEnvironment(RunningEnvironmentNames.IntegrationTest);

        builder.ConfigureTestServices(services =>
        {
            services.AddSingleton<IClock>(clock=>ClockStub.Instantiate());
        });

    }

    protected override void Dispose(bool disposing)
    {
        var scope = base.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<FrameworkDbContext>();
        dbContext.Database.EnsureDeleted();
    }
}
