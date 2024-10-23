using Framework.Core.ServiceContracts;
using Games.Contract._Shared.Enums;
using Games.Domain.PlayerAggregate.Contracts;

namespace Games.Domain.PlayerAggregate.Arguments;

public record PlayerRegistrationArgs
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public Gender Gender { get; set; }
    public required string UserId { get; set; }
    public required IPlayerDuplicationRegistrationDetector DuplicationRegistrationDetector { get; set; }
    public required IIdGenerator IdGenerator { get; set; }
    public required IClock Clock { get; set; }
}