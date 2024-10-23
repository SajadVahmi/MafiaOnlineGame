using Framework.Core.ServiceContracts;
using Games.Contract.Enums;

namespace Games.Domain.PlayerAggregate.Arguments;

public class PlayerChangeGenderArgs
{
    public Gender Gender { get; set; }
    public required IIdGenerator IdGenerator { get; set; }
    public required IClock Clock { get; set; }
}