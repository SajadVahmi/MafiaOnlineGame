using Framework.Core.ServiceContracts;
using Games.Contract.Enums;
using Games.Domain.PlayerAggregate.Services;

namespace Games.Domain.PlayerAggregate.Arguments;

public class PlayerRegistrationArgs
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public Gender Gender { get; set; }
    public required string UserId { get; set; }
    public required IIdGenerator IdGenerator { get; set; }
    public required IClock Clock { get; set; }
}