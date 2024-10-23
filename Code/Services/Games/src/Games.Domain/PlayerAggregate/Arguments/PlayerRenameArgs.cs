using Framework.Core.ServiceContracts;

namespace Games.Domain.PlayerAggregate.Arguments;

public class PlayerRenameArgs
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required IIdGenerator IdGenerator { get; set; }
    public required IClock Clock { get; set; }
}