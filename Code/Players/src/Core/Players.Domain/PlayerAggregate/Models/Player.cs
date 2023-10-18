using Framework.Core.Domian.Aggregates;
using Players.Contracts.Enums;
using Players.Domain.PlayerAggregate.Events;

namespace Players.Domain.PlayerAggregate.Models;

public partial class Player : AggregateRoot<PlayerId>
{
    public static async Task<Player> RegisterAsync(PlayerRegisterArgs args, CancellationToken cancellationToken = default)
    {

        await PlayerGuards.CheckRegistrationDuplicationAsync(args, cancellationToken);

        return new Player(args);

    }

    protected Player(PlayerRegisterArgs args)
    {

        Causes(new PlayerIsRegistred(
             id: args.Id.Value,
             firstName: args.FirstName,
             lastName: args.LastName,
             birthDate: args.BirthDate,
             gender: args.Gender,
             userId: args.UserId,
             registerDateTime: args.Clock.Now(),
             idProvider: args.IdProvider,
             clock: args.Clock
            ));
    }


    public string FirstName { get; private set; }

    public string LastName { get; private set; }

    public DateOnly BirthDate { get; private set; }

    public Gender Gender { get; private set; }

    public DateTimeOffset RegisterDateTime { get; private set; }

    public string UserId { get; private set; }

}
