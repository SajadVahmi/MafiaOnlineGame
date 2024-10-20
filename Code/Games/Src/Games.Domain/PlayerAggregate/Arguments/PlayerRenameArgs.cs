using Framework.Core.ServiceContracts;
using Games.Domain.Contracts.Enums;

namespace Games.Domain.PlayerAggregate.Arguments;

public class PlayerRenameArgs
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required IIdGenerator IdGenerator { get; set; }
    public required IClock Clock { get; set; }
}