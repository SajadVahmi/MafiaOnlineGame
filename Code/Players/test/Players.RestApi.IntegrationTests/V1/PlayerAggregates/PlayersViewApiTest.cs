using FluentAssertions;
using Framework.Test.Api.Fixtures;
using Players.ApplicationServices.PlayerAggregate.Dto;
using System.Net;
using System.Net.Http.Json;
using Players.SharedTestClasses.PlayerAggregate.Factories;

namespace Players.RestApi.IntegrationTests.V1.PlayerAggregates;

public class PlayersViewApiTest(FrameworkWebApplicationFactory<Program> factory) 
    : PlayersApiTestBase(factory)

{
    [Fact(DisplayName = "Should return 200 status code with player info when user registered")]
    public async Task ShouldReturn200StatusCodeWithPlayerInfo_WhenUserRegistered()
    {
        //Arrange
        var registeredPlayer = await PlayerAggregateFactory.CreateAPlayerAsync(AuthUserId);

        Factory.InitialDatabase(registeredPlayer);

        var viewPlayerUrl = Endpoints.Version1.View.Replace("{playerId}", registeredPlayer.Id.Value.ToString());


        //Act
        var response = await Client.GetAsync(viewPlayerUrl);

        var responseBody = await response.Content.ReadFromJsonAsync<PlayerDto>();


        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        responseBody.Id.Should().Be(registeredPlayer.Id.Value.ToString());

        responseBody.FirstName.Should().Be(registeredPlayer.FirstName);

        responseBody.LastName.Should().Be(registeredPlayer.LastName);

        responseBody.BirthDate.Should().Be(registeredPlayer.BirthDate);

        responseBody.RegisterDateTime.Should().Be(registeredPlayer.RegisterDateTime);
    }

    [Fact(DisplayName = "Should return 404 status code when user did not register")]
    public async Task ShouldReturn404StatusCode_WhenUserDidNotRegister()
    {
        //Arrange
        var playerIdThatDidNotRegister = 213413214567;

        var viewPlayerUrl = Endpoints.Version1.View.Replace("{playerId}", playerIdThatDidNotRegister.ToString());

        //Act
        var response = await Client.GetAsync(viewPlayerUrl);


        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
}