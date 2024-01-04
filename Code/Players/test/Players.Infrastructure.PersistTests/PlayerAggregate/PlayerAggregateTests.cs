using FluentAssertions;
using Players.Domain.PlayerAggregate.Data;
using Players.Domain.PlayerAggregate.Models;
using Players.Infrastructure.PersistTests.Shared;
using Players.Persistence.SQL.Repositories;
using Players.SharedTestClasess.PlayerAggregate.Factories;

namespace Players.Infrastructure.PersistTests.PlayerAggregate;

public class PlayerAggregateTests : IClassFixture<PlayersPersistTestFixture>
{
    IPlayerRepository _playerRepository;

    public PlayerAggregateTests(PlayersPersistTestFixture fixture)
    {

        _playerRepository = new PlayerRepository(fixture.DbContext, fixture.SequenceService);
    }

    [Fact]
    public async Task Repository_Can_Persist_And_Load_Aggregate()
    {

        //Arrange
        Player playerForPersist = await PlayerAggregateFactory.CreateAPlayer();


        //Act
        await _playerRepository.RegisterAsync(playerForPersist);

        var persistedPlayer = await _playerRepository.LoadAsync(playerForPersist.Id);

        //Assert
        persistedPlayer.Should().NotBeNull();

        persistedPlayer.Should().BeEquivalentTo(playerForPersist);


    }
}
