using IDP.Shared.IdentityStore.DbContexts;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
namespace IDP.Administration.Api.Test.Integration.Fixtures;

public class CustomWebApplicationFactory<TProgram>
: WebApplicationFactory<TProgram>, IDisposable where TProgram : class
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("Test");
    }

    protected override void Dispose(bool disposing)
    {
        var scope = base.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<IdpDbContext>();
        dbContext.Database.EnsureDeleted();
    }

}
