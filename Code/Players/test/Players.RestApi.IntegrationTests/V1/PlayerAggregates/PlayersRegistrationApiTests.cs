using FluentAssertions;
using Framework.Test.Api.Fixtures;
using Players.Domain.PlayerAggregate.Models;
using Players.RestApi.IntegrationTests.V1.PlayerAggregates.Factories;
using Players.RestApi.V1.PlayerAggregate.Responses.Register;
using Players.SharedTestClasess.PlayerAggregate.Factories;
using System.Net;
using System.Net.Http.Json;



namespace Players.RestApi.IntegrationTests.V1.PlayerAggregates;

public class PlayersRegistrationApiTests : PlayersApiTestBase
{

    public PlayersRegistrationApiTests(FrameworkWebApplicationFactory<Program> factory) : base(factory)
    {

    }

    [Fact(DisplayName = "Register player api should returns 201 response when request is valid.")]
    public async Task RegisterPlayerApi_ShouldReturnsCreatedHttpSatusCode_WhenRequestIsValid()
    {
        //Arrange
        var registerRequestBody = PlayerApiRequestFactory.CreatePlayerRegistrationRequest();

        //Act
        var response = await Client.PostAsync(Endpoints.Registration, registerRequestBody);

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.Created);

    }


    [Fact(DisplayName = "Register player api should return registred player when http response is success.")]
    public async Task RegisterPlayerApi_ShouldReturnRegistredPlayer_WhenHttpResponseIsSuccess()
    {

        //Arrange
        var registerRequestBody =
            PlayerApiRequestFactory.CreatePlayerRegistrationRequest(player =>
              player.WithFirstName(Sajad.FirstName)
                    .WithLastName(Sajad.LastName)
                    .WithBirthDate(Sajad.BirthDate)
                    .WithGender(Sajad.Gender));

        DateTimeOffset? currentDateTime = Clock.Now();

        //Act
        var response = await Client.PostAsync(Endpoints.Registration, registerRequestBody);

        var responseBody = await response.Content.ReadFromJsonAsync<PlayerRegisterationResponse>();

        //Assert
        responseBody.Should().NotBeNull();

        responseBody.Id.Should().NotBeNullOrEmpty();

        responseBody.Id.Should().NotBe("0");

        responseBody.FirstName.Should().Be(Sajad.FirstName);

        responseBody.LastName.Should().Be(Sajad.LastName);

        responseBody.BirthDate.Should().Be(Sajad.BirthDate);

        responseBody.Gender.Should().Be(Sajad.Gender);

        responseBody.RegisterDateTime.Should().Be(currentDateTime);

    }


    [Fact(DisplayName = "Register player api should return conflic status code when a user wants to register twice")]
    public async Task RegisterPlayerApi_ShouldReturnConflicStatusCode_WhenAUserWantsToRegisterTwice()
    {
        //Arrange
        var registredPlayer = await PlayerAggregateFactory.CreateAPlayer(AuthUserId);

        Factory.InitialDatabase<Player>(registredPlayer);

        var registerRequestBody =
           PlayerApiRequestFactory.CreatePlayerRegistrationRequest(player =>
             player.WithFirstName(SomeBody.FirstName)
                   .WithLastName(SomeBody.LastName)
                   .WithBirthDate(SomeBody.BirthDate)
                   .WithGender(SomeBody.Gender));

        //Act
        var response = await Client.PostAsync(Endpoints.Registration, registerRequestBody);

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.Conflict);
    }

}
