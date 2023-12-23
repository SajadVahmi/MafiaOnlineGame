using FluentAssertions;
using Players.RestApi.IntegrationTests.V1.PlayerAggregates.Factories;
using Players.RestApi.IntegrationTests.V1.PlayerAggregates.Fixtures;
using Players.RestApi.V1.PlayerAggregate.Responses.Register;
using System.Net;
using System.Net.Http.Json;
using static Players.RestApi.IntegrationTests.V1.PlayerAggregates.TestData.PlayerTestData;

namespace Players.RestApi.IntegrationTests.V1.PlayerAggregates.TestClasses;

public class PlayersApiTests : PlayersApiTestBase
{

    public PlayersApiTests(PlayersWebApplicationFactory factory) : base(factory)
    {

    }

    [Fact(DisplayName = "Register player api should returns 201 response when request is valid.")]
    public async Task RegisterPlayerApi_ShouldReturnsCreatedHttpSatusCode_WhenRequestIsValid()
    {
        //Arrange
        var registerRequestBody = PlayerRequestFactory.CreatePlayerRegistrationRequest();

        //Act
        var response = await Client.PostAsync(Endpoints.Registration, registerRequestBody);

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.Created);

    }

    [Fact]
    public async Task RegisterPlayerApi_ShouldReturnRegistredPlayer_WhenHttpResponseIsSuccess()
    {

        //Arrange
        var registerRequestBody =
            PlayerRequestFactory.CreatePlayerRegistrationRequest(player =>
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


}
