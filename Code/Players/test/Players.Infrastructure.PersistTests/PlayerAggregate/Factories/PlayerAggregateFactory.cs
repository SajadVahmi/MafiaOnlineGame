using Framework.Core.Services;
using Framework.Test.Stubs;
using NSubstitute;
using Players.Domain.PlayerAggregate.Models;
using Players.Domain.PlayerAggregate.Services;
using Players.Infrastructure.PersistTests.Shared;
using static Players.Infrastructure.PersistTests.PlayerAggregate.TestData.PlayerAggregateTestData;

namespace Players.Infrastructure.PersistTests.PlayerAggregate.Factories;

public static class PlayerAggregateFactory
{
    public static async Task<Player> CreateAPlayerForPersist()
    {
        IDuplicateRegistrationCheckService duplicateRegistrationCheckService = Substitute.For<IDuplicateRegistrationCheckService>();

        duplicateRegistrationCheckService.CheckAsync(Arg.Any<string>()).Returns(false);

        var PlayerAggregateArgs = new PlayerRegisterArgs()
        {
            Id = Sajad.Id,
            FirstName = Sajad.FirstName,
            LastName = Sajad.LastName,
            BirthDate = Sajad.BirthDate,
            Gender = Sajad.Gender,
            UserId = Sajad.UserId,
            Clock = new ClockStub(SharedTestData.DateTime.CurrentDateTime),
            IdProvider = new GuidEventIdProvider(),
            DuplicateRegistrationCheckService = duplicateRegistrationCheckService
        };

        var player = await Player.RegisterAsync(PlayerAggregateArgs);

        return player;
    }
}
