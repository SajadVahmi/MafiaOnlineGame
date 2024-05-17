using System.Security.Cryptography.X509Certificates;
using Framework.Core.Domain.Aggregates;
using Games.Domain.Contracts.DomainEvents.PlayerAggregate;
using Games.Domain.Contracts.Enums;
using Games.Domain.PlayerAggregate.Arguments;
using Guard= Games.Domain.PlayerAggregate.Models.PlayerGuards;

namespace Games.Domain.PlayerAggregate.Models;

public class Player:AggregateRoot<PlayerFamily>
{
    public static async Task<Player> RegisterAsync(PlayerRegistrationArgs args,CancellationToken cancellationToken=default)
    {
        var player = new Player(args);

        await Guard.AvoidDoubleRegistrationAsync(player, args.DuplicateRegistrationCheckService, cancellationToken);

        return player;
    }

    private Player(){}

    protected Player(PlayerRegistrationArgs args)
    {
        Causes(new PlayerRegistered(
            Id:args.IdGenerator.GetNewString(),
            EventId:args.IdGenerator.GetNewGuid(),
            WhenItHappened:args.Clock.Now(),
            Name:args.Name,
            Family:args.Family,
            Gender:args.Gender,
            UserId:args.UserId));
    }
    
    public PlayerName Name { get;private set; }
    public PlayerName Family { get;private set; }
    public Gender Gender { get;private set; }
    public PlayerUserId UserId { get;private set; }
}