using Framework.Core.Domain.Aggregates;
using Players.Contracts.Enums;
using Players.Domain.PlayerAggregate.Events;

namespace Players.Domain.PlayerAggregate.Models;

public partial class Player : AggregateRoot<PlayerId>
{
    public static async Task<Player> RegisterAsync(PlayerRegisterArgs args, CancellationToken cancellationToken = default)
    {

        await PlayerGuards.AvoidDoubleRegistrationAsync(args, cancellationToken);

        return new Player(args);

    }

    protected Player() { }

    protected Player(PlayerRegisterArgs args)
    {

        Causes(new PlayerIsRegistered(
             playerId: args.Id.Value,
             firstName: args.FirstName,
             lastName: args.LastName,
             birthDate: args.BirthDate,
             gender: args.Gender,
             userId: args.UserId,
             registerDateTime: args.Clock.Now(),
             eventId: args.IdProvider.Get(),
             whenItHappened: args.Clock.Now()
            ));
    }


    public string FirstName { get; private set; } = null!;

    public string LastName { get; private set; } = null!;

    public DateOnly BirthDate { get; private set; }

    public Gender Gender { get; private set; }

    public DateTimeOffset RegisterDateTime { get; private set; }

    public string UserId { get; private set; } = null!;


    public void ChangeProfile(PlayerChangeProfileArgs args)
    {
        Causes(new PlayerProfileChanged(
             playerId: Id.Value,
             firstName: args.FirstName,
             lastName: args.LastName,
             birthDate: args.BirthDate,
             gender: args.Gender,
             eventId: args.IdProvider.Get(),
             whenItHappened: args.Clock.Now()
            ));
    }
}
