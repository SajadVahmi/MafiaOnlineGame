using Framework.Core.Contracts;
using Games.Domain.Contracts.Enums;
using Games.Domain.PlayerAggregate.Services;

namespace Games.Domain.PlayerAggregate.Arguments;

public class PlayerRegistrationArgs
{
    public required string Name { get; set; }
    public required string Family { get; set; }
    public Gender Gender { get; set; }
    public required string UserId { get; set; }
    public required IIdGenerator IdGenerator { get; set; }
    public required IClock Clock { get; set; }
    public required IDuplicateRegistrationCheckService DuplicateRegistrationCheckService { get; set; }


}