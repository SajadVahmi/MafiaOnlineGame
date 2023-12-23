using Framework.Core.Contracts;
using Players.Contracts.Enums;
using Players.Domain.PlayerAggregate.Services;

namespace Players.Domain.PlayerAggregate.Models;

public class PlayerRegisterArgs
{
    public required PlayerId Id { get; set; }

    public required string FirstName { get; set; }

    public required string LastName { get; set; }

    public DateOnly BirthDate { get; set; }

    public Gender Gender { get; set; }

    public required string UserId { get; set; }

    public required IClock Clock { get; set; }

    public required IEventIdProvider IdProvider { get; set; }

    public required IDuplicateRegistrationCheckService DuplicateRegistrationCheckService { get; set; }

}