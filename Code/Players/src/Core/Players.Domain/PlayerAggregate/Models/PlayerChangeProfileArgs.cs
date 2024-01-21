using Framework.Core.Contracts;
using Players.Contracts.Enums;

namespace Players.Domain.PlayerAggregate.Models;

public class PlayerChangeProfileArgs
{
    public required string FirstName { get; set; }

    public required string LastName { get; set; }

    public DateOnly BirthDate { get; set; }

    public Gender Gender { get; set; }

    public required IClock Clock { get; set; }

    public required IEventIdProvider IdProvider { get; set; }

}
