﻿using FluentAssertions;
using Framework.Presentation.RestApi;
using Framework.Test.Api.Fixtures;
using Players.Contracts.Resources;
using Players.RestApi.IntegrationTests.V1.PlayerAggregates.Factories;
using Players.RestApi.V1.PlayerAggregate.Requests.Register;
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

    [Fact(DisplayName = "Should returns 201 response when request is valid.")]
    public async Task ShouldReturnsCreatedHttpSatusCode_WhenRequestIsValid()
    {
        //Arrange
        var registerRequestBody = PlayerApiRequestFactory.CreatePlayerRegistrationRequest();

        //Act
        var response = await Client.PostAsync(Endpoints.Registration, registerRequestBody);

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.Created);

    }


    [Fact(DisplayName = "Should return registred player when http response is success.")]
    public async Task ShouldReturnRegistredPlayer_WhenHttpResponseIsSuccess()
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


    [Fact(DisplayName = "Should return 409 status code when a user wants to register twice")]
    public async Task ShouldReturnConflicStatusCode_WhenAUserWantsToRegisterTwice()
    {
        //Arrange
        var registredPlayer = await PlayerAggregateFactory.CreateAPlayerAsync(AuthUserId);

        Factory.InitialDatabase(registredPlayer);

        var registerRequestBody =
           PlayerApiRequestFactory.CreatePlayerRegistrationRequest();

        //Act
        var response = await Client.PostAsync(Endpoints.Registration, registerRequestBody);

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.Conflict);
    }


    [Fact(DisplayName = "Should return api error object with specified message and code when a user wants to register twice")]
    public async Task ShouldReturnApiErrorObjectWithSpecifiedMessageAndCode_WhenAUserWantsToRegisterTwice()
    {
        //Arrange
        var registredPlayer = await PlayerAggregateFactory.CreateAPlayerAsync(AuthUserId);

        Factory.InitialDatabase(registredPlayer);

        var registerRequestBody =
           PlayerApiRequestFactory.CreatePlayerRegistrationRequest();

        //Act
        var response = await Client.PostAsync(Endpoints.Registration, registerRequestBody);

        var responseBody = await response.Content.ReadFromJsonAsync<ApiError>();

        //Assert
        responseBody.Code.Should().Be(PlayersCodes.Player100TheUserAlreadyRegistred);

        responseBody.Message.Should().Be(PlayersResource.Player100TheUserAlreadyRegistred);
    }

    [Fact(DisplayName = "Should returns 400 response when user registration request has invalid data")]
    public async Task ShouldReturns400Response_WhenUserRegistrationRequestHasInvalidData()
    {
        //Arrange
        var request = PlayerApiRequestFactory.CreatePlayerRegistrationRequest(player =>
             player.WithFirstName(null)
        );

        //Act
        var response = await Client.PostAsync(Endpoints.Registration, request);

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Theory(DisplayName = "Should return api validation error with specified property name and message when user registration request has invalid data")]
    [MemberData(nameof(GetInvalidRegistrationDataAndItsAssertion))]
    public async Task ShouldReturnApiValidationErrorWithSpecifiedPropertyNmaeAndMessage_WhenUserRegistrationRequestHasInvalidData(StringContent request, Action<ApiValidationError> assertion)
    {

        //Act
        var response = await Client.PostAsync(Endpoints.Registration, request);

        var responseBody = await response.Content.ReadFromJsonAsync<List<ApiValidationError>>();

        //Assert
        assertion.Invoke(responseBody.First());
    }

    public static IEnumerable<object[]> GetInvalidRegistrationDataAndItsAssertion()
     => new List<object[]>
     {
       new object[]
       {
            PlayerApiRequestFactory.CreatePlayerRegistrationRequest(player =>player.WithFirstName(null)),
            new Action<ApiValidationError>(error =>
            {
                error.PropertyName.Should().Be(nameof(PlayerRegistrationRequest.FirstName));
                error.Messages.Single().Should().Be(PlayersResource.Player104FirstNameIsRequired);
            })
       },
       new object[]
       {
            PlayerApiRequestFactory.CreatePlayerRegistrationRequest(player =>player.WithFirstName("ab")),
            new Action<ApiValidationError>(error =>
            {
                error.PropertyName.Should().Be(nameof(PlayerRegistrationRequest.FirstName));
                error.Messages.Single().Should().Be(PlayersResource.Player105FirstNameLengthIsInvalid);
            })
       },
       new object[]
       {
            PlayerApiRequestFactory.CreatePlayerRegistrationRequest(player =>player.WithFirstName("ThisSentenceIncludesANameWithMoreThanfiftyCharacters")),
            new Action<ApiValidationError>(error =>
            {
                error.PropertyName.Should().Be(nameof(PlayerRegistrationRequest.FirstName));
                error.Messages.Single().Should().Be(PlayersResource.Player105FirstNameLengthIsInvalid);
            })
       },
       new object[]
       {
            PlayerApiRequestFactory.CreatePlayerRegistrationRequest(player =>player.WithLastName(null)),
            new Action<ApiValidationError>(error =>
            {
                error.PropertyName.Should().Be(nameof(PlayerRegistrationRequest.LastName));
                error.Messages.Single().Should().Be(PlayersResource.Player108LastNameIsRequired);
            })
       },
       new object[]
       {
            PlayerApiRequestFactory.CreatePlayerRegistrationRequest(player =>player.WithLastName("ab")),
            new Action<ApiValidationError>(error =>
            {
                error.PropertyName.Should().Be(nameof(PlayerRegistrationRequest.LastName));
                error.Messages.Single().Should().Be(PlayersResource.Player109LastNameLengthIsInvalid);
            })
       },
       new object[]
       {
            PlayerApiRequestFactory.CreatePlayerRegistrationRequest(player =>player.WithLastName("ThisSentenceIncludesALastNameWithMoreThanfiftyCharacters")),
            new Action<ApiValidationError>(error =>
            {
                error.PropertyName.Should().Be(nameof(PlayerRegistrationRequest.LastName));
                error.Messages.Single().Should().Be(PlayersResource.Player109LastNameLengthIsInvalid);
            })
       },
        new object[]
       {
            PlayerApiRequestFactory.CreatePlayerRegistrationRequest(player =>player.WithBirthDate(null)),
            new Action<ApiValidationError>(error =>
            {
                error.PropertyName.Should().Be(nameof(PlayerRegistrationRequest.BirthDate));
                error.Messages.Single().Should().Be(PlayersResource.Player103BirthDateIsRequired);
            })
       },
       new object[]
       {
            PlayerApiRequestFactory.CreatePlayerRegistrationRequest(player =>player.WithBirthDate(default(DateOnly))),
            new Action<ApiValidationError>(error =>
            {
                error.PropertyName.Should().Be(nameof(PlayerRegistrationRequest.BirthDate));
                error.Messages.Single().Should().Be(PlayersResource.Player102BirthDateIsInvalid);
            })
       },
       new object[]
       {
            PlayerApiRequestFactory.CreatePlayerRegistrationRequest(player =>player.WithGender(null)),
            new Action<ApiValidationError>(error =>
            {
                error.PropertyName.Should().Be(nameof(PlayerRegistrationRequest.Gender));
                error.Messages.Single().Should().Be(PlayersResource.Player107GenderIsRequired);
            })
       },
       new object[]
       {
            PlayerApiRequestFactory.CreatePlayerRegistrationRequest(player =>player.WithGender(0)),
            new Action<ApiValidationError>(error =>
            {
                error.PropertyName.Should().Be(nameof(PlayerRegistrationRequest.Gender));
                error.Messages.Single().Should().Be(PlayersResource.Player106GenderIsInvalid);
            })
       }
     };

}
