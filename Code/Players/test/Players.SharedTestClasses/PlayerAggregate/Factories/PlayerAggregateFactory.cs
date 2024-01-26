using NSubstitute;
using Players.Contracts.Enums;
using Players.Domain.PlayerAggregate.Models;
using Players.Domain.PlayerAggregate.Services;
using Players.SharedTestClasses.PlayerAggregate.Builders;

namespace Players.SharedTestClasses.PlayerAggregate.Factories;

public static class PlayerAggregateFactory
{
    public static async Task<Player> CreateAPlayerAsync()
    {
        IDuplicateRegistrationCheckService duplicateRegistrationCheckService = Substitute.For<IDuplicateRegistrationCheckService>();

        duplicateRegistrationCheckService.CheckAsync(Arg.Any<string>()).Returns(false);

        var PlayerAggregateArgs = PlayerRegisterArgsTestBuilder.Instantiate().Build();

        var player = await Player.RegisterAsync(PlayerAggregateArgs);

        return player;
    }

    public static async Task<Player> CreateAFemailPlayerAsync()
    {
        IDuplicateRegistrationCheckService duplicateRegistrationCheckService = Substitute.For<IDuplicateRegistrationCheckService>();

        duplicateRegistrationCheckService.CheckAsync(Arg.Any<string>()).Returns(false);

        var PlayerAggregateArgs = PlayerRegisterArgsTestBuilder.Instantiate().WithGender(Gender.Female).Build();

        var player = await Player.RegisterAsync(PlayerAggregateArgs);

        return player;
    }

    public static async Task<Player> CreateAPlayerAsync(string userId)
    {
        IDuplicateRegistrationCheckService duplicateRegistrationCheckService = Substitute.For<IDuplicateRegistrationCheckService>();

        duplicateRegistrationCheckService.CheckAsync(Arg.Any<string>()).Returns(false);

        var PlayerAggregateArgs = PlayerRegisterArgsTestBuilder.Instantiate()
            .WithUserId(userId)
            .Build();

        var player = await Player.RegisterAsync(PlayerAggregateArgs);

        return player;
    }

    public static async Task<Player> CreateAFemailPlayerAsync(string userId)
    {
        IDuplicateRegistrationCheckService duplicateRegistrationCheckService = Substitute.For<IDuplicateRegistrationCheckService>();

        duplicateRegistrationCheckService.CheckAsync(Arg.Any<string>()).Returns(false);

        var PlayerAggregateArgs = PlayerRegisterArgsTestBuilder.Instantiate()
            .WithGender(Gender.Female)
            .WithUserId(userId)
            .Build();

        var player = await Player.RegisterAsync(PlayerAggregateArgs);

        return player;
    }
}
