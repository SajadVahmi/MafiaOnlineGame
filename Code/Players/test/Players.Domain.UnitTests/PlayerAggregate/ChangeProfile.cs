using FluentAssertions;
using Players.Domain.PlayerAggregate.Events;
using Players.Domain.PlayerAggregate.Models;
using Players.SharedTestClasess.PlayerAggregate.Builders;
using Players.SharedTestClasess.PlayerAggregate.Factories;
using System.Security.Cryptography.X509Certificates;
using static Players.SharedTestClasess.PlayerAggregate.Data.PlayerTestData;

namespace Players.Domain.UnitTests.PlayerAggregate
{
    public class ChangeProfile
    {
        [Fact(DisplayName = "Each player can change his or her profile")]
        public async Task each_player_can_change_his_or_her_profile()
        {
            //Arrang
            var player = await PlayerAggregateFactory.CreateAFemailPlayerAsync();

            var playerChangeProfileArgs = PlayerChangeProfileArgsTestBuilder.Instantiate()
             .WithFirstName("UpdatedFirstName")
             .WithLastName("UpdatedLastName")
             .WithBirthDate(player.BirthDate.AddMonths(1))
             .WithGender(Contracts.Enums.Gender.Male)
             .Build();

            //Act
            player.ChangeProfile(playerChangeProfileArgs);


            //Assert
            player.FirstName.Should().Be(playerChangeProfileArgs.FirstName);

            player.LastName.Should().Be(playerChangeProfileArgs.LastName);

            player.Gender.Should().Be(playerChangeProfileArgs.Gender);

            player.BirthDate.Should().Be(playerChangeProfileArgs.BirthDate);


        }

        [Fact(DisplayName = "Player profile changed happens when player changes it")]
        public async Task player_profile_changed_happens_when_player_changes_it()
        {
            var player = await PlayerAggregateFactory.CreateAFemailPlayerAsync();

            var playerUpdateArgs = PlayerChangeProfileArgsTestBuilder.Instantiate()
             .WithFirstName("UpdatedFirstName")
             .WithLastName("UpdatedLastName")
             .WithBirthDate(player.BirthDate.AddMonths(1))
             .WithGender(Contracts.Enums.Gender.Male)
             .Build();

            var expectedDomainEvent = new PlayerProfileChanged(
              playerId: player.Id.Value,
              firstName: playerUpdateArgs.FirstName,
              lastName: playerUpdateArgs.LastName,
              birthDate: playerUpdateArgs.BirthDate,
              gender: playerUpdateArgs.Gender,
              eventId: playerUpdateArgs.IdProvider.Get(),
              whenItHappened: playerUpdateArgs.Clock.Now()
              );

            //Act
            player.ChangeProfile(playerUpdateArgs);


            //Assert
            player.GetEvents().Should().ContainEquivalentOf(expectedDomainEvent);
        }


    }
}
