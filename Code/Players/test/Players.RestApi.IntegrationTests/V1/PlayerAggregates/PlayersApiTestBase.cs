using Framework.Core.Contracts;
using Framework.Test.Api.Builders;
using Framework.Test.Api.Fixtures;
using Framework.Test.Api.Handlers.AuthHandlers;
using Framework.Test.Stubs;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;


namespace Players.RestApi.IntegrationTests.V1.PlayerAggregates;


public class PlayersApiTestBase : IClassFixture<FrameworkWebApplicationFactory<Program>>
{
    protected IClock Clock;

    protected FrameworkWebApplicationFactory<Program> Factory;

    protected HttpClient Client;

    protected string AuthUserId;

    public PlayersApiTestBase(FrameworkWebApplicationFactory<Program> factory)
    {
        Clock = ClockStub.Instantiate();

        Factory = factory;

        Client = CreateClient();


    }

    private HttpClient CreateClient()
    {
        AuthUserId = new Random().Next(1, int.MaxValue).ToString();

        MockAuthUser authenticatedUser = MockAuthUserBuilder.Instantiate()
            .WithClaim("sub", AuthUserId)
            .WithClaim("scope", PlayerBcScopeName)
            .Build();

        return Factory.WithWebHostBuilder(builder =>
        {
            builder.ConfigureTestServices(services =>
            {
                services.AddSingleton(Clock);

                services.AddTestAuthentication();

                services.AddScoped(_ => authenticatedUser);
            });

        }).CreateClient();
    }
}
