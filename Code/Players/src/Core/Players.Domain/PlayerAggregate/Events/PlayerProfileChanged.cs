using Framework.Core.Domian.Events;
using Players.Contracts.Enums;

namespace Players.Domain.PlayerAggregate.Events;

public class PlayerProfileChanged : DomainEvent
{

    public PlayerProfileChanged(long playerId, string firstName, string lastName, DateOnly birthDate, Gender gender,
                              string eventId,DateTimeOffset whenItHappened) : base(eventId, whenItHappened)
    {
        PlayerId = playerId;

        FirstName = firstName;

        LastName = lastName;

        BirthDate = birthDate;

        Gender = gender;

    }

    public long PlayerId { get; private set; }

    public string FirstName { get; private set; }

    public string LastName { get; private set; }

    public DateOnly BirthDate { get; private set; }

    public Gender Gender { get; private set; }
}
