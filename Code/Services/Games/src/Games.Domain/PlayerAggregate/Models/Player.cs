using Framework.Core.Domain.Aggregates;
using Framework.Core.Domain.ValueObjects;
using Games.Contract.DomainEvents;
using Games.Contract.Enums;
using Games.Domain.PlayerAggregate.Arguments;

namespace Games.Domain.PlayerAggregate.Models;

public partial class Player:AggregateRoot<EntityId>
{
    public static Player Register(PlayerRegistrationArgs args)
    {
        var player = new Player(args);

        return player;
    }

    private Player(){}

    protected Player(PlayerRegistrationArgs args)
    {
        Causes(new PlayerRegistered(
            EventId: args.IdGenerator.GetNewId(),
            Id:args.IdGenerator.GetNewId(),
            FirstName: args.FirstName,
            LastName: args.LastName,
            Gender:args.Gender,
            UserId:args.UserId,
            WhenItHappened: args.Clock.Now()));
    }

    public PlayerName Name { get; private set; } = null!;
    public Gender Gender { get;private set; }
    public PlayerUserId UserId { get;private set; } = null!;

    public void Rename(PlayerRenameArgs args)
    {
        Causes(new PlayerRenamed(
            EventId: args.IdGenerator.GetNewId(),
            Id: Id.Value,
            FirstName: args.FirstName,
            LastName: args.LastName,
            WhenItHappened: args.Clock.Now()));
    }

    public void ChangeGender(PlayerChangeGenderArgs args)
    {
        Causes(new PlayerGenderChanged(
            EventId: args.IdGenerator.GetNewId(),
            Id:Id.Value,
            Gender: args.Gender,
            WhenItHappened: args.Clock.Now()));
    }
}