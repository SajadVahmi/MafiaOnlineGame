using Framework.Core.Domain.Events;
using Players.Contracts.Enums;

namespace Players.Domain.PlayerAggregate.Events;

public class PlayerProfileChanged(
    long playerId,
    string firstName,
    string lastName,
    DateOnly birthDate,
    Gender gender,
    string eventId,
    DateTimeOffset whenItHappened)
    : DomainEvent(eventId, whenItHappened)
{
    public long PlayerId { get; private set; } = playerId;

    public string FirstName { get; private set; } = firstName;

    public string LastName { get; private set; } = lastName;

    public DateOnly BirthDate { get; private set; } = birthDate;

    public Gender Gender { get; private set; } = gender;
}
