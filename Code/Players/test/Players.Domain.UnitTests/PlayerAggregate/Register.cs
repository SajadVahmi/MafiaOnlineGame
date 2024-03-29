﻿using FluentAssertions;
using Players.Contracts.Resources;
using Players.Domain.PlayerAggregate.Events;
using Players.Domain.PlayerAggregate.Exceptions;
using Players.Domain.PlayerAggregate.Models;
using Players.Domain.PlayerAggregate.Services;
using Players.SharedTestClasses.PlayerAggregate.Builders;
using Players.SharedTestClasses.PlayerAggregate.Factories;

namespace Players.Domain.UnitTests.PlayerAggregate;

public class Register
{

    [Fact(DisplayName = "Each user can register as a player.")]
    public async Task each_user_can_register_as_a_player()
    {
        //Arrange
        var playerRegisterArgs = PlayerRegisterArgsTestBuilder.Instantiate().Build();

        //Act
        var player = await Player.RegisterAsync(playerRegisterArgs);

        //Assert
        player.Id.Should().Be(playerRegisterArgs.Id);

        player.FirstName.Should().Be(playerRegisterArgs.FirstName);

        player.LastName.Should().Be(playerRegisterArgs.LastName);

        player.BirthDate.Should().Be(playerRegisterArgs.BirthDate);

        player.Gender.Should().Be(playerRegisterArgs.Gender);

        player.UserId.Should().Be(playerRegisterArgs.UserId);

        player.RegisterDateTime.Should().Be(playerRegisterArgs.Clock.Now());

    }


    [Fact(DisplayName = "Registration happens when the registration information is correct")]
    public async Task registration_happens_when_the_registration_information_is_correct()
    {
        //Arrange
        PlayerRegisterArgs playerRegisterArgs = PlayerRegisterArgsTestBuilder.Instantiate().Build();

        var expectedDomainEvent = new PlayerIsRegistered(
            playerId: playerRegisterArgs.Id.Value,
            firstName: playerRegisterArgs.FirstName,
            lastName: playerRegisterArgs.LastName,
            birthDate: playerRegisterArgs.BirthDate,
            gender: playerRegisterArgs.Gender,
            userId: playerRegisterArgs.UserId,
            registerDateTime: playerRegisterArgs.Clock.Now(),
            eventId: playerRegisterArgs.IdProvider.Get(),
            whenItHappened: playerRegisterArgs.Clock.Now()
            );

        //Act
        Player player = await Player.RegisterAsync(playerRegisterArgs);

        //Assert
        player.GetEvents().Should().ContainEquivalentOf(expectedDomainEvent);

    }


    [Fact(DisplayName = "Each user can register only once")]
    public async Task each_user_can_register_only_once()
    {
        //Arrange
        IDuplicateRegistrationCheckService duplicateRegistrationCheckService = PlayerDomainServicesTestFactory
            .CreateDuplicateRegistrationCheckServiceThatReturnsTrueResult();

        PlayerRegisterArgs playerRegisterArgs = PlayerRegisterArgsTestBuilder.Instantiate()
            .WithDuplicateRegistrationCheckService(duplicateRegistrationCheckService)
            .Build();

        //Act
        Func<Task> registration = async () => await Player.RegisterAsync(playerRegisterArgs);

        //Assert
        var exception = await registration.Should().ThrowExactlyAsync<TheUserAlreadyRegisteredException>();

        exception.Which.Message.Should().Be(PlayersResource.Player100TheUserAlreadyRegistred);

        exception.Which.Code.Should().Be(PlayersCodes.Player100TheUserAlreadyRegistred);


    }


}
