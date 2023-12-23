using Framework.Persistence.EF;
using Newtonsoft.Json;
using Players.Domain.PlayerAggregate.Models;
using Players.RestApi.IntegrationTests.V1.PlayerAggregates.TestBuilders;
using System.Text;

namespace Players.RestApi.IntegrationTests.V1.PlayerAggregates.Factories;

public static class PlayerRequestFactory
{

    public static StringContent CreatePlayerRegistrationRequest()
    {
        var builder = PlayerRegistrationRequestBuilder.Instantiate();

        return new StringContent(JsonConvert.SerializeObject(builder.Build()), Encoding.UTF8, "application/json");
    }

    public static StringContent CreatePlayerRegistrationRequest(Action<PlayerRegistrationRequestBuilder> playerRegistrationConfiguration)
    {
        var builder = PlayerRegistrationRequestBuilder.Instantiate();

        playerRegistrationConfiguration.Invoke(builder);

        return new StringContent(JsonConvert.SerializeObject(builder.Build()), Encoding.UTF8, "application/json");
    }

  

  
}