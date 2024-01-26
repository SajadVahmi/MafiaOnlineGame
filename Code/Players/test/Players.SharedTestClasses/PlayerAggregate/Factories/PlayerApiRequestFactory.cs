using System.Text;
using Newtonsoft.Json;
using Players.SharedTestClasses.PlayerAggregate.Builders;

namespace Players.SharedTestClasses.PlayerAggregate.Factories;

public static class PlayerApiRequestFactory
{

    public static StringContent CreatePlayerRegistrationRequest()
    {
        var builder = PlayerRegistrationRequestBuilder.Instantiate();

        return new StringContent(JsonConvert.SerializeObject(builder.Build()), Encoding.UTF8, "application/json");
    }

    public static StringContent CreatePlayerChangeProfileRequest()
    {
        var builder = PlayerChangeProfileRequestBuilder.Instantiate();

        return new StringContent(JsonConvert.SerializeObject(builder.Build()), Encoding.UTF8, "application/json");
    }

    public static StringContent CreatePlayerRegistrationRequest(Action<PlayerRegistrationRequestBuilder> playerRegistrationConfiguration)
    {
        var builder = PlayerRegistrationRequestBuilder.Instantiate();

        playerRegistrationConfiguration.Invoke(builder);

        return new StringContent(JsonConvert.SerializeObject(builder.Build()), Encoding.UTF8, "application/json");
    }

    public static StringContent CreatePlayerChangeProfileRequest(Action<PlayerChangeProfileRequestBuilder> playerChangeProfileConfiguration)
    {
        var builder = PlayerChangeProfileRequestBuilder.Instantiate();

        playerChangeProfileConfiguration.Invoke(builder);

        return new StringContent(JsonConvert.SerializeObject(builder.Build()), Encoding.UTF8, "application/json");
    }




}