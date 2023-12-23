using FluentAssertions;
using Players.Domain.PlayerAggregate.Data;
using Players.Domain.PlayerAggregate.Models;
using Players.Infrastructure.PersistTests.PlayerAggregate.Factories;
using Players.Infrastructure.PersistTests.Shared;
using Players.Persistence.SQL.Repositories;

namespace Players.Infrastructure.PersistTests.PlayerAggregate.TestClasess;

public class PlayerAggregateTests : IClassFixture<PlayersPersistTestFixture>
{
    IPlayerRepository _playerRepository;

    public PlayerAggregateTests(PlayersPersistTestFixture fixture)
    {

        _playerRepository = new PlayerRepository(fixture.DbContext, fixture.SequenceService);
    }

    [Fact]
    public async Task Repository()
    {

        //Arrange
        Player playerForPersist = await PlayerAggregateFactory.CreateAPlayerForPersist();


        //Act
        await _playerRepository.RegisterAsync(playerForPersist);

        var persistedPlayer = await _playerRepository.LoadAsync(playerForPersist.Id);

        //Assert
        persistedPlayer.Should().NotBeNull();

        persistedPlayer.Should().BeEquivalentTo(playerForPersist);


    }
}
