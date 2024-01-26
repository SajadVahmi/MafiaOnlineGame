using FluentAssertions;
using Players.Domain.PlayerAggregate.Data;
using Players.Domain.PlayerAggregate.Models;
using Players.Infrastructure.PersistTests.Shared;
using Players.Persistence.SQL.Repositories;
using Players.SharedTestClasses.PlayerAggregate.Builders;
using Players.SharedTestClasses.PlayerAggregate.Factories;


namespace Players.Infrastructure.PersistTests.PlayerAggregate;

public class PlayerAggregateTests(PlayersPersistTestFixture fixture) : IClassFixture<PlayersPersistTestFixture>
{
    readonly IPlayerRepository _playerRepository = new PlayerRepository(fixture.DbContext, fixture.SequenceService);

    [Fact]
    public async Task player_repository_can_persist_and_load_aggregate()
    {

        //Arrange
        Player playerForPersist = await PlayerAggregateFactory.CreateAPlayerAsync();


        //Act
        _playerRepository.Register(playerForPersist);

        await _playerRepository.SaveChangesAsync();

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

         _playerRepository.Register(player);

         await _playerRepository.SaveChangesAsync();

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
