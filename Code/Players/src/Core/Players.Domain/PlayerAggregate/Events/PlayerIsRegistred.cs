using Framework.Core.Contracts;
using Framework.Core.Domian.Events;
using Players.Contracts.Enums;

namespace Players.Domain.PlayerAggregate.Events;

public class PlayerIsRegistred : DomainEvent
{

    public PlayerIsRegistred(long id, string firstName, string lastName, DateOnly birthDate, Gender gender,
                             DateTimeOffset registerDateTime, string userId, IEventIdProvider idProvider,
                             IClock clock) : base(idProvider, clock)
    {
        Id = id;

        FirstName = firstName;

        LastName = lastName;

        BirthDate = birthDate;

        Gender = gender;

        RegisterDateTime = registerDateTime;

        UserId = userId;
    }

    public long Id { get; private set; }

    public string FirstName { get; private set; }

    public string LastName { get; private set; }

    public DateOnly BirthDate { get; private set; }

    public Gender Gender { get; private set; }

    public DateTimeOffset RegisterDateTime { get; private set; }

    public string UserId { get; private set; }
}
