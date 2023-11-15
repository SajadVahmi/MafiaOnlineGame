using Framework.Test.Api.Fixtures;

namespace Players.RestApi.IntegrationTests.V1.PlayerAggregates.Fixtures;


public class PlayersApiTransactionRollbackTestBase:IClassFixture<PlayersWebApplicationFactory>
{

    protected PlayersWebApplicationFactory Factory;

    public PlayersApiTransactionRollbackTestBase(PlayersWebApplicationFactory factory)
    {

        Factory = factory;
      
    }

}
