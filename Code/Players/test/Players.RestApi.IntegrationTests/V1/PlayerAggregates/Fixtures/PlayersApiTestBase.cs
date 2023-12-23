using Framework.Core.Contracts;
using Framework.Test.Api.Builders;
using Framework.Test.Api.Handlers.AuthHandlers;
using Framework.Test.Stubs;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;

namespace Players.RestApi.IntegrationTests.V1.PlayerAggregates.Fixtures;


public class PlayersApiTestBase : IClassFixture<PlayersWebApplicationFactory>
{
    protected IClock Clock;

    protected PlayersWebApplicationFactory Factory;

    protected HttpClient Client;

    public PlayersApiTestBase(PlayersWebApplicationFactory factory)
    {
        Clock = ClockStub.Instantiate();
        Factory = factory;

        Client = CreateClient();


    }

    private HttpClient CreateClient()
    {
        var randomUserId = new Random().Next(1, int.MaxValue).ToString();

        MockAuthUser authenticatedUser = MockAuthUserBuilder.Instantiate()
            .WithClaim("sub", randomUserId)
            .WithClaim("scope", "players")
            .Build();

        return Factory.WithWebHostBuilder(builder =>
        {
            builder.ConfigureTestServices(services =>
            {
                services.AddSingleton<IClock>(Clock);

                services.AddTestAuthentication();

                services.AddScoped(_ => authenticatedUser);
            });

        }).CreateClient();
    }
}
