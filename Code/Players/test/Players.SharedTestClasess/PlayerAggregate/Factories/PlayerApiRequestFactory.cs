﻿using Newtonsoft.Json;
using Players.SharedTestClasess.PlayerAggregate.Builders;
using System.Text;

namespace Players.RestApi.IntegrationTests.V1.PlayerAggregates.Factories;

public static class PlayerApiRequestFactory
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