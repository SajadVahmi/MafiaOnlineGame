using FluentAssertions;
using Players.Domain.PlayerAggregate.Data;
using Players.Domain.PlayerAggregate.Models;
using Players.Infrastructure.PersistTests.Shared;
using Players.Persistence.SQL.Repositories;
using Players.SharedTestClasess.PlayerAggregate.Builders;
using Players.SharedTestClasess.PlayerAggregate.Factories;
using System.Numerics;

namespace Players.Infrastructure.PersistTests.PlayerAggregate;

public class PlayerAggregateTests : IClassFixture<PlayersPersistTestFixture>
{
    IPlayerRepository _playerRepository;

    public PlayerAggregateTests(PlayersPersistTestFixture fixture)
    {

        _playerRepository = new PlayerRepository(fixture.DbContext, fixture.SequenceService);
    }

    [Fact]
    public async Task player_repository_can_persist_and_load_aggregate()
    {

        //Arrange
        Player playerForPersist = await PlayerAggregateFactory.CreateAPlayerAsync();


        //Act
        await _playerRepository.RegisterAsync(playerForPersist);

        var persistedPlayer = await _playerRepository.LoadAsync(playerForPersist.Id);

        //Assert
        persistedPlayer.Should().NotBeNull();

        persistedPlayer.Should().BeEquivalentTo(playerForPersist);


    }

    [Fact]
    public async Task player_repository_can_save_changes_on_aggreggate()
    {
        //Arrange
        Player player = await PlayerAggregateFactory.CreateAFemailPlayerAsync();

        await _playerRepository.RegisterAsync(player);

        var playerChangeProfileArgs=PlayerChangeProfileArgsTestBuilder.Instantiate()
            .WithFirstName("UpdatedFirstName")
             .WithLastName("UpdatedLastName")
             .WithBirthDate(player.BirthDate.AddMonths(1))
             .WithGender(Contracts.Enums.Gender.Male)
             .Build();

        player.ChangeProfile(playerChangeProfileArgs);

        //Act
        var persistedPlayer = await _playerRepository.LoadAsync(player.Id,player.UserId);

        //Assert
        persistedPlayer.Should().NotBeNull();

        persistedPlayer.Should().BeEquivalentTo(player);
    }
}
