using FluentAssertions;
using Framework.Presentation.RestApi;
using Framework.Test.Api.Fixtures;
using Microsoft.AspNetCore.Http;
using Players.Contracts.Enums;
using Players.Contracts.Resources;
using Players.Domain.PlayerAggregate.Models;
using Players.RestApi.IntegrationTests.V1.PlayerAggregates.Factories;
using Players.RestApi.V1.PlayerAggregate.Requests.ChangeProfile;
using Players.SharedTestClasess.PlayerAggregate.Factories;
using System.Net;
using System.Net.Http.Json;

namespace Players.RestApi.IntegrationTests.V1.PlayerAggregates;

public class PlayersChangeProfileApiTest : PlayersApiTestBase
{
    public PlayersChangeProfileApiTest(FrameworkWebApplicationFactory<Program> factory) : base(factory)
    {
    }

    [Fact(DisplayName = "Should returns 204 response when request is valid.")]
    public async Task ShouldReturnsNoContentHttpSatusCode_WhenRequestIsValid()
    {
        //Arrange
        var registredPlayer = await PlayerAggregateFactory.CreateAFemailPlayerAsync(AuthUserId);

        Factory.InitialDatabase(registredPlayer);

        var changeProfileRequestBody =
           PlayerApiRequestFactory.CreatePlayerChangeProfileRequest();

        var changeProfileUrl = Endpoints.ChangeProfile.Replace("{playerId}", registredPlayer.Id.Value.ToString());

        //Act
        var response = await Client.PutAsync(changeProfileUrl, changeProfileRequestBody);

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.NoContent);

    }

    [Fact(DisplayName = "Should changes players profile when request is valid.")]
    public async Task ShouldChangesPlayersProfile_WhenRequestIsValid()
    {
        //Arrange
        var registredPlayer = await PlayerAggregateFactory.CreateAFemailPlayerAsync(AuthUserId);

        Factory.InitialDatabase(registredPlayer);

        var changeProfileRequestBody =
           PlayerApiRequestFactory.CreatePlayerChangeProfileRequest(player =>
             player.WithFirstName("UpdatedFirstName")
                   .WithLastName("UpdatedLastName")
                   .WithBirthDate(registredPlayer.BirthDate.AddMonths(1))
                   .WithGender(Gender.Male));

        var changeProfileUrl = Endpoints.ChangeProfile.Replace("{playerId}", registredPlayer.Id.Value.ToString());

        //Act
        var response = await Client.PutAsync(changeProfileUrl, changeProfileRequestBody);

        var updatedPlayer = Factory.LoadFromDatabase<Player, PlayerId>(registredPlayer.Id);

        //Assert
        updatedPlayer.FirstName.Should().Be("UpdatedFirstName");

        updatedPlayer.LastName.Should().Be("UpdatedLastName");

        updatedPlayer.BirthDate.Should().Be(registredPlayer.BirthDate.AddMonths(1));

        updatedPlayer.Gender.Should().Be(Gender.Male);


    }

    [Fact(DisplayName = "Should return api error object with specified message and code when player not found")]
    public async Task ShouldReturnApiErrorObjectWithSpecifiedMessageAndCode_WhenPlayerNotFound()
    {
        //Arrange
        var notExistPlayerId = 102024584;

        var changeProfileRequestBody =
           PlayerApiRequestFactory.CreatePlayerChangeProfileRequest();

        var changeProfileUrl = Endpoints.ChangeProfile.Replace("{playerId}", notExistPlayerId.ToString());

        //Act
        var response = await Client.PutAsync(changeProfileUrl, changeProfileRequestBody);

        var responseBody = await response.Content.ReadFromJsonAsync<ApiError>();

        //Assert
        responseBody.Code.Should().Be(PlayersCodes.Player101ThePlayerNotFound);

        responseBody.Message.Should().Be(PlayersResource.Player101ThePlayerNotFound);


    }

    [Fact(DisplayName = "Should returns 404 response when playerId not exist.")]
    public async Task ShouldReturns404Response_WhenPlayerIdNotExist()
    {
        //Arrange
        var notExistPlayerId = 102024584;

        var changeProfileRequestBody =
           PlayerApiRequestFactory.CreatePlayerChangeProfileRequest();

        var changeProfileUrl = Endpoints.ChangeProfile.Replace("{playerId}", notExistPlayerId.ToString());

        //Act
        var response = await Client.PutAsync(changeProfileUrl, changeProfileRequestBody);

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);

    }

    [Fact(DisplayName = "Should return 400 response when user change profile request has invalid data")]
    public async Task ShouldReturn400Response_WhenChangeProfileRequestHasInvalidData()
    {
        //Arrange
        var changeProfileRequest =
           PlayerApiRequestFactory.CreatePlayerChangeProfileRequest(player =>
              player.WithFirstName(null)
           );

        var changeProfileUrl = Endpoints.ChangeProfile.Replace("{playerId}", "1");


        //Act
        var response = await Client.PutAsync(changeProfileUrl, changeProfileRequest);

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Theory(DisplayName = "Should return api validation error with specified property name and message when user registration request has invalid data")]
    [MemberData(nameof(GetInvalidChangeProfileDataAndItsAssertion))]
    public async Task ShouldReturnApiValidationErrorWithSpecifiedPropertyNmaeAndMessage_WhenChangeProfileRequestHasInvalidData(StringContent request, Action<ApiError> assertion)
    {
        //Arrange
        var changeProfileUrl = Endpoints.ChangeProfile.Replace("{playerId}", "1");

        //Act
        var response = await Client.PutAsync(changeProfileUrl, request);

        var responseBody = await response.Content.ReadFromJsonAsync<ApiError>();

        //Assert
        assertion.Invoke(responseBody);
    }

    public static IEnumerable<object[]> GetInvalidChangeProfileDataAndItsAssertion()
    => new List<object[]>
    {
       new object[]
       {
            PlayerApiRequestFactory.CreatePlayerChangeProfileRequest(player =>player.WithFirstName(null)),
            new Action<ApiError>(error =>
            {
                error.Code.Should().Be(PlayersCodes.RequestValidation400);
                error.Message.Should().Be(PlayersResource.RequestValidation400);
                error.MetaData?.Single().Key.Should().Be(nameof(PlayerChangeProfileRequest.FirstName));
                error.MetaData?.Single().Value.Should().Contain(PlayersResource.Player104FirstNameIsRequired);
            })
       },
       //new object[]
       //{
       //     PlayerApiRequestFactory.CreatePlayerChangeProfileRequest(player =>player.WithFirstName("ab")),
       //     new Action<ApiError>(error =>
       //     {
       //         error.Code.Should().Be(PlayersCodes.RequestValidation400);
       //         error.Message.Should().Be(PlayersResource.RequestValidation400);
       //         error.MetaData?.Single().Key.Should().Be(nameof(PlayerChangeProfileRequest.FirstName));
       //         error.MetaData?.Single().Value.Should().Contain(PlayersResource.Player105FirstNameLengthIsInvalid);
       //     })
       //},
       //new object[]
       //{
       //     PlayerApiRequestFactory.CreatePlayerChangeProfileRequest(player =>player.WithFirstName("ThisSentenceIncludesANameWithMoreThanfiftyCharacters")),
       //     new Action<ApiError>(error =>
       //     {
       //         error.Code.Should().Be(PlayersCodes.RequestValidation400);
       //         error.Message.Should().Be(PlayersResource.RequestValidation400);
       //         error.MetaData?.Single().Key.Should().Be(nameof(PlayerChangeProfileRequest.FirstName));
       //         error.MetaData?.Single().Value.Should().Contain(PlayersResource.Player105FirstNameLengthIsInvalid);
       //     })
       //},
       //new object[]
       //{
       //     PlayerApiRequestFactory.CreatePlayerChangeProfileRequest(player =>player.WithLastName(null)),
       //     new Action<ApiError>(error =>
       //     {
       //         error.Code.Should().Be(PlayersCodes.RequestValidation400);
       //         error.Message.Should().Be(PlayersResource.RequestValidation400);
       //         error.MetaData?.Single().Key.Should().Be(nameof(PlayerChangeProfileRequest.LastName));
       //         error.MetaData?.Single().Value.Should().Contain(PlayersResource.Player108LastNameIsRequired);
       //     })
       //},
       //new object[]
       //{
       //     PlayerApiRequestFactory.CreatePlayerChangeProfileRequest(player =>player.WithLastName("ab")),
       //     new Action<ApiError>(error =>
       //     {
       //         error.Code.Should().Be(PlayersCodes.RequestValidation400);
       //         error.Message.Should().Be(PlayersResource.RequestValidation400);
       //         error.MetaData?.Single().Key.Should().Be(nameof(PlayerChangeProfileRequest.LastName));
       //         error.MetaData?.Single().Value.Should().Contain(PlayersResource.Player109LastNameLengthIsInvalid);
       //     })
       //},
       //new object[]
       //{
       //     PlayerApiRequestFactory.CreatePlayerChangeProfileRequest(player =>player.WithLastName("ThisSentenceIncludesALastNameWithMoreThanfiftyCharacters")),
       //     new Action<ApiError>(error =>
       //     {
       //         error.Code.Should().Be(PlayersCodes.RequestValidation400);
       //         error.Message.Should().Be(PlayersResource.RequestValidation400);
       //         error.MetaData?.Single().Key.Should().Be(nameof(PlayerChangeProfileRequest.LastName));
       //         error.MetaData?.Single().Value.Should().Contain(PlayersResource.Player109LastNameLengthIsInvalid);
       //     })
       //},
       // new object[]
       //{
       //     PlayerApiRequestFactory.CreatePlayerChangeProfileRequest(player =>player.WithBirthDate(null)),
       //     new Action<ApiError>(error =>
       //     {
       //         error.Code.Should().Be(PlayersCodes.RequestValidation400);
       //         error.Message.Should().Be(PlayersResource.RequestValidation400);
       //         error.MetaData?.Single().Key.Should().Be(nameof(PlayerChangeProfileRequest.BirthDate));
       //         error.MetaData?.Single().Value.Should().Contain(PlayersResource.Player103BirthDateIsRequired);
       //     })
       //},
       //new object[]
       //{
       //     PlayerApiRequestFactory.CreatePlayerChangeProfileRequest(player =>player.WithBirthDate(default(DateOnly))),
       //     new Action<ApiError>(error =>
       //     {
       //         error.Code.Should().Be(PlayersCodes.RequestValidation400);
       //         error.Message.Should().Be(PlayersResource.RequestValidation400);
       //         error.MetaData?.Single().Key.Should().Be(nameof(PlayerChangeProfileRequest.BirthDate));
       //         error.MetaData?.Single().Value.Should().Contain(PlayersResource.Player102BirthDateIsInvalid);
       //     })
       //},
       //new object[]
       //{
       //     PlayerApiRequestFactory.CreatePlayerChangeProfileRequest(player =>player.WithGender(null)),
       //     new Action<ApiError>(error =>
       //     {
       //         error.Code.Should().Be(PlayersCodes.RequestValidation400);
       //         error.Message.Should().Be(PlayersResource.RequestValidation400);
       //         error.MetaData?.Single().Key.Should().Be(nameof(PlayerChangeProfileRequest.Gender));
       //         error.MetaData?.Single().Value.Should().Contain(PlayersResource.Player107GenderIsRequired);
       //     })
       //},
       //new object[]
       //{
       //     PlayerApiRequestFactory.CreatePlayerChangeProfileRequest(player =>player.WithGender(0)),
       //     new Action<ApiError>(error =>
       //     {
       //         error.Code.Should().Be(PlayersCodes.RequestValidation400);
       //         error.Message.Should().Be(PlayersResource.RequestValidation400);
       //         error.MetaData?.Single().Key.Should().Be(nameof(PlayerChangeProfileRequest.Gender));
       //         error.MetaData?.Single().Value.Should().Contain(PlayersResource.Player106GenderIsInvalid);
       //     })
       //}
    };
}
