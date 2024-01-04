using Framework.Core.Services;
using Framework.Test.Stubs;
using NSubstitute;
using Players.Domain.PlayerAggregate.Models;
using Players.Domain.PlayerAggregate.Services;
using Players.SharedTestClasess.PlayerAggregate.Builders;
using Players.SharedTestClasess.Shared.Data;

namespace Players.SharedTestClasess.PlayerAggregate.Factories;

public static class PlayerAggregateFactory
{
    public static async Task<Player> CreateAPlayer()
    {
        IDuplicateRegistrationCheckService duplicateRegistrationCheckService = Substitute.For<IDuplicateRegistrationCheckService>();

        duplicateRegistrationCheckService.CheckAsync(Arg.Any<string>()).Returns(false);

        var PlayerAggregateArgs = PlayerRegisterArgsTestBuilder.Instantiate().Build();
        //{
        //    Id = Sajad.Id,
        //    FirstName = Sajad.FirstName,
        //    LastName = Sajad.LastName,
        //    BirthDate = Sajad.BirthDate,
        //    Gender = Sajad.Gender,
        //    UserId = Sajad.UserId,
        //    Clock = new ClockStub(DateTimeTestData.Friday29December2023),
        //    IdProvider = new GuidEventIdProvider(),
        //    DuplicateRegistrationCheckService = duplicateRegistrationCheckService
        //};

        var player = await Player.RegisterAsync(PlayerAggregateArgs);

        return player;
    }
}
